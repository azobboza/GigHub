using GigHub.Models;
using System;
namespace GigHub.Repositories
{
    interface IFollowingRepository
    {
        Following GetFollowing(string userId, string followeeId);
    }
}
