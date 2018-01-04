var followingController = function (followingService)
{
    var button;
    var init = function (container) {
        //$(".js-toggle-following").click(toggleFollowing)
        $(container).on("click", ".js-toggle-following", toggleFollowing);
    }

    var toggleFollowing = function (e)
    {
        button = $(e.target);
        var id = button.attr("data-artist-id")
        if (button.hasClass("btn-default"))
            followingService.createFollowing(id, done, fail)
        else
            followingService.deleteFollowing(id, done, fail)
    }
    
    var done = function () {
        var text = (button.text() == "Following") ? "Follow" : "Following"
        button.toggleClass("btn-info").toggleClass("btn-default").text(text);
    }

    var fail = function () {
        alert("Something wrong!")
    }

    return {
        init: init
    }
    
}(FollowingService);