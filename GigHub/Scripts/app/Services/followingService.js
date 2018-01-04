var FollowingService = function () {
    
    var createFollowing = function (id, done, fail) {
        $.post("/api/following", { ArtistId: id })
                    .done(done)
                    .fail(fail)
    }

    var deleteFollowing = function (id, done, fail) {
        $.ajax({
            url: "/api/following/" + id,
            method: "DELETE"
        })
        .done(done)
        .fail(fail)
    }

    return {
        createFollowing: createFollowing,
        deleteFollowing: deleteFollowing
    }

}();