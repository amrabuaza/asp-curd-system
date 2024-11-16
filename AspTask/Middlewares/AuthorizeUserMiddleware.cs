using AspTask.Services;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace AspTask.Middlewares
{
    /// <summary>
    /// Middleware to handle user authorization by checking if the user is logged in.
    /// Redirects to login or home page depending on the user's login state and request path.
    /// </summary>
    public class AuthorizeUserMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthorizeUserMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next middleware in the request pipeline.</param>
        public AuthorizeUserMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Invokes the middleware to authorize the user and manage the redirection based on the login state.
        /// </summary>
        /// <param name="context">The HTTP context for the current request.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        public async Task InvokeAsync(HttpContext context)
        {
            bool isUserLoggedIn = UserSessionManager.IsUserLoggedIn();
            string requestPath = context.Request.Path;
            bool isAccountPaths = requestPath.Equals("/Account/Login") || requestPath.Equals("/Account/Signup");

            // Ensure paths other than the home page or index are authorized.
            if (!requestPath.Equals("/") && !requestPath.Equals("/Home/Index"))
            {
                if (isUserLoggedIn)
                {
                    // Redirect logged-in users to home if they try to access login or signup.
                    if (isAccountPaths)
                    {
                        context.Response.Redirect("/Home/Index");
                        return;
                    }
                }
                else
                {
                    // Redirect non-logged-in users to login page if they try to access other than login/signup.
                    if (!isAccountPaths)
                    {
                        context.Response.Redirect("/Account/Login");
                        return;
                    }
                }
            }
            await _next(context);
        }
    }// end class
}// end namespace
