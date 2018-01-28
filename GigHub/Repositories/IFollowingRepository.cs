using GigHub.Models;
using System;
namespace GigHub.Repositories
{
    public interface IFollowingRepository
    {
        Following GetFollowing(string userId, string followeeId);
    }
}
