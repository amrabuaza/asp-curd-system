$(document).ready(function () {
    // when user click on button create post we need to display post form as popup
    $('#createPostBtn').on('click', function () {
        $('#createPostModal').modal('show');
    });

    /*
     * When user click on delete post icon
     * we need to display popup for confirm delete post
     */ 
    $('.delete-post-button').on('click', function () {
        let targetPostId = $(this).data('target-post-id');
        $('#deletePostModal').modal('show');
        $('#deletePostModal #post-id').val(targetPostId);
    });

    // hide delete post modal when user click on cancel delete button
    $('#hide-delete-modal').on('click', function () {
        $('#deletePostModal').modal('hide');
    });

    /*
     * When user click on edit post button need to fetch post data
     * and display data inside popup to allow user to edit post
     */
    $(document).on("click", ".edit-post-button", function () {
        const postId = $(this).data("target-post-id");

        $.ajax({
            url: `/Posts/GetPost/${postId}`,
            type: "GET",
            success: function (post) {
                $("#editPostId").val(post.id);
                $("#editPostTitle").val(post.title);
                $("#editPostDescription").val(post.description);
                $("#editPostIsActive").val(post.isActive.toString());
                $("#editPostModal").modal("show");
            },
            error: function () {
                alert("Error loading post data.");
            },
        });
    });

});
