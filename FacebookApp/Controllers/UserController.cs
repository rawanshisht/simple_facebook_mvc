using System;
using System.Collections.Generic;
using System.Linq;
using FacebookApp.ViewModel;

using System.Threading.Tasks;
using FacebookApp.Data;
using FacebookApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace FacebookApp.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly Microsoft.AspNetCore.Identity.UserManager<User> _userManager;
        private readonly IHostingEnvironment hostingEnvironment;

        public UserController(ApplicationDbContext db, Microsoft.AspNetCore.Identity.UserManager<User> UserManager, IHostingEnvironment ihosting)
        {
            context = db;
            this._userManager = UserManager;
            this.hostingEnvironment = ihosting;
        }

        #region Profile
        public IActionResult Profile(string id)
        {
            //var userid = id;
            //if (userid == null)
            //    userid = _userManager.GetUserId(User);
            //var user = context.Users.Find(userid);
            //if (userid == null)
            //    return RedirectToAction("Home");
            //return View(user);

            //--------------------------------------

            //Profile info
            if (id == null)
                id = _userManager.GetUserId(User);
            var userFromDb = context.Users.Include(u => u.Posts).SingleOrDefault(u => u.Id == id);
            var userVM = new UserViewModel()
            {
                Nickname = userFromDb.Nickname,
                Bio = userFromDb.Bio,
                BirthDate = userFromDb.BirthDate,
                Gender = userFromDb.Gender,
                Image = userFromDb.Image,
                Email = userFromDb.Email,
                UserId = userFromDb.Id,
            };

            IEnumerable<Post> postsList = context.Posts.Where(p => p.IsDeleted == false);
            var postVMLst = new List<PostsViewModel>();
            foreach (var post in postsList)
            {
                //var currentUserId = _userManager.GetUserId(User);
                if (post.UserId == id)
                {
                    var postVM = new PostsViewModel();
                    postVM.Content = post.Content;
                    postVM.IsDeleted = post.IsDeleted;
                    postVM.PostId = post.Id;
                    postVM.UserId = post.UserId;
                    postVM.PostingDate = post.PostingDate;

                    IEnumerable<UserLikesPost> likesList = context.UserLikesPosts.Where(l => l.PostId == post.Id && l.IsLiked == true);
                    IEnumerable<UserCommentsOnPost> commentsList = context.UserCommentsOnPosts.Where(c => c.PostId == post.Id && c.IsDeleted == false);

                    var likesVMLst = new List<LikesViewModel>();
                    var commentsVMLst = new List<CommentsViewModel>();

                    foreach (var like in likesList)
                    {
                        var likeVM = new LikesViewModel();
                        likeVM.UserId = like.UserId;
                        likeVM.PostId = like.PostId;
                        likeVM.IsLiked = like.IsLiked;
                        likesVMLst.Add(likeVM);
                    }
                    postVM.likesVMList = likesVMLst;

                    foreach (var comment in commentsList)
                    {
                        var commentVM = new CommentsViewModel();
                        commentVM.Content = comment.Content;
                        commentVM.UserId = comment.UserId;
                        commentVM.IsDeleted = comment.IsDeleted;
                        commentVM.PostId = comment.PostId;
                        commentVM.CommentId = comment.CommentId;
                        commentsVMLst.Add(commentVM);
                    }
                    postVM.commentsVMList = commentsVMLst;


                    postVMLst.Add(postVM);
                }
            }

            userVM.Posts = postVMLst;

            //if (userFromDb.Posts != null)
            //    foreach (var item in userFromDb.Posts)
            //    {
            //        userVM.Posts.Add(new PostsViewModel()
            //        {
            //            Content = item.Content,
            //            IsDeleted = item.IsDeleted,
            //            PostingDate = item.PostingDate,
            //            UserId = id,
            //            PostId = item.Id
            //        });
            //    }
            //Get friends of the user
            var friendships = context.UserHasFriends.Where(f => f.UserId == id || f.FriendId == id).ToList();
            if (friendships.Count > 0)
            {
                foreach (var friendship in friendships)
                {
                    if (friendship.Status != FriendRequestStatus.NotFriend)
                    {
                        var trueFriendId = "";
                        var friendshipStatus = friendship.Status;

                        if (friendship.FriendId == id)
                        {
                            trueFriendId = friendship.UserId;
                            if (friendship.Status == FriendRequestStatus.SentAndPending)
                            {
                                friendshipStatus = FriendRequestStatus.ReceivedAndPending;
                            }
                            else if (friendship.Status == FriendRequestStatus.ReceivedAndPending)
                            {
                                friendshipStatus = FriendRequestStatus.SentAndPending;
                            }
                        }
                        else if (friendship.UserId == id)
                        {
                            trueFriendId = friendship.FriendId;
                        }
                        //var friendFromDb = context.Users.Find(friendship.FriendId ?? friendship.UserId);
                        var friendFromDb = context.Users.Find(trueFriendId);

                        userVM.Friends.Add(new FriendsViewModel()
                        {
                            FriendId = friendFromDb.Id,
                            Nickname = friendFromDb.Nickname,
                            Email = friendFromDb.Email,
                            Bio = friendFromDb.Bio,
                            Gender = friendFromDb.Gender,
                            Image = friendFromDb.Image,
                            Status = friendshipStatus
                        });
                    }
                }
            }
            //else
            //    userVM.Status = FriendRequestStatus.NotFriend;
            return View(userVM);
        }
        #endregion

        #region Edit User
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();
            return PartialView(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Id,UserName,Email,BirthDate,Bio,Gender,Nickname")] User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    User u = await _userManager.FindByIdAsync(user.Id);
                    u.BirthDate = user.BirthDate;
                    u.Bio = user.Bio;
                    u.Gender = user.Gender;
                    u.Nickname = user.Nickname;
                    await _userManager.UpdateAsync(u);
                    //context.Update(user);
                    //context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return RedirectToAction(actionName: "Profile", controllerName: "User");
        }
        #endregion

        #region Edit Picture
        public async Task<IActionResult> EditPicture(string id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();
            var userVM = new UserViewModel()
            {
                UserId = user.Id,
                Email = user.Email,
                BirthDate = user.BirthDate,
                Image = user.Image,
                Bio = user.Bio,
                Gender = user.Gender,
                Nickname = user.Nickname
            };
            return PartialView(userVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPicture(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(model);
                var userid = _userManager.GetUserId(User);
                var user = context.Users.Find(userid);
                //user.Image = uniqueFileName;
                user.Image = "~/Images/ProfilePictures/" + uniqueFileName;
                await _userManager.UpdateAsync(user);
                //context.Entry(user).State = EntityState.Modified;
                //context.SaveChanges();
                return RedirectToAction("Profile");
            }
            return View();
        }

        private string UploadedFile(UserViewModel model)
        {
            string uniqueFileName = null;

            if (model.ImageFile != null)
            {
                string s = Path.Combine("Images", "ProfilePictures");
                string uploadsFolder = Path.Combine(hostingEnvironment.WebRootPath, s);
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImageFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ImageFile.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
        #endregion

        #region Search With Button
        public IActionResult SearchForAFriend(string searchString)
        {
            if (searchString != null)
            {
                var usersLst = from u in context.Users
                               select u;

                if (!String.IsNullOrEmpty(searchString))
                    usersLst = usersLst.Where(s => s.UserName.ToLower().Contains(searchString.ToLower()));

                return View(usersLst.ToList());

            }
            return View(context.Users.ToList());
        }

        [HttpPost]
        public string SearchForAFriend(string searchString, bool notUsed)
        {
            return "From [HttpPost]Index: filter on " + searchString;
        }
        #endregion

        #region Friend
        public IActionResult SendFriendRequest(string id)
        {
            //Sender >> Current User
            //Reciever >> userObj
            var userObj = context.Users.Find(id);
            var currentUserId = _userManager.GetUserId(User);
            var senderObj = context.Users.SingleOrDefault(u => u.Id == currentUserId);
            var isAFriend = context.UserHasFriends.SingleOrDefault(u => u.UserId == currentUserId && u.FriendId == id);
            if (senderObj != null && isAFriend == null)
                context.UserHasFriends.Add(new UserHasFriend()
                {
                    UserId = senderObj.Id,
                    FriendId = userObj.Id,
                    Status = FriendRequestStatus.SentAndPending
                });
            else if (isAFriend.Status == FriendRequestStatus.NotFriend)
                isAFriend.Status = FriendRequestStatus.SentAndPending;
            context.SaveChanges();
            return RedirectToAction("Profile", "User", new { id = id });
        }

        public IActionResult AcceptFriendRequest(string id)
        {
            //Sender >> Current User
            //Reciever >> userObj
            var userObj = context.Users.Find(id);
            var currentUserId = _userManager.GetUserId(User);
            var senderObj = context.Users.SingleOrDefault(u => u.Id == currentUserId);
            var isAFriend = context.UserHasFriends.SingleOrDefault(u => u.UserId == currentUserId && u.FriendId == id);
            if (senderObj != null && isAFriend != null)
            {
                isAFriend.Status = FriendRequestStatus.Friend;
                context.SaveChanges();
            }
            return RedirectToAction("Profile", "User", new { id = id });
        }

        public IActionResult CancelFriendRequest(string id)
        {
            //Sender >> Current User
            //Reciever >> userObj
            var userObj = context.Users.Find(id);
            var currentUserId = _userManager.GetUserId(User);
            var senderObj = context.Users.SingleOrDefault(u => u.Id == currentUserId);
            var isAFriend = context.UserHasFriends.SingleOrDefault(u => u.UserId == currentUserId && u.FriendId == id);
            if (senderObj != null && isAFriend != null)
            {
                isAFriend.Status = FriendRequestStatus.NotFriend;
                context.SaveChanges();
            }
            return RedirectToAction("Profile", "User", new { id = id });
        }

        public IActionResult UnFriend(string id)
        {
            //Sender >> Current User
            //Reciever >> userObj
            var userObj = context.Users.Find(id);
            var currentUserId = _userManager.GetUserId(User);
            var senderObj = context.Users.SingleOrDefault(u => u.Id == currentUserId);
            var isAFriend = context.UserHasFriends.SingleOrDefault(u => u.UserId == currentUserId && u.FriendId == id);
            if (senderObj != null && isAFriend != null)
            {
                isAFriend.Status = FriendRequestStatus.NotFriend;
                context.SaveChanges();
            }
            return RedirectToAction("Profile", "User", new { id = id });
        }
        #endregion

        private bool UserExists(string id)
        {
            return context.Users.Any(e => e.Id == id);
        }
    }
}