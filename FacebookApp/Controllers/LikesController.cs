using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FacebookApp.Data;
using FacebookApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.IO.MemoryMappedFiles;
using Microsoft.AspNetCore.Identity;

namespace FacebookApp.Controllers
{
    public class LikesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public LikesController(ApplicationDbContext context, UserManager<User> UserManager, SignInManager<User> SignInManager)
        {
            _context = context;
            _userManager = UserManager;
            _signInManager = SignInManager;
        }

        #region index
        //// GET: Likes
        //public async Task<IActionResult> Index()
        //{
        //    var applicationDbContext = _context.UserLikesPosts.Include(u => u.Post).Include(u => u.User);
        //    return View(await applicationDbContext.ToListAsync());
        //} 
        #endregion

        // GET: Likes/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userLikesPost = _context.UserLikesPosts.ToList().Where(m => m.PostId == Int32.Parse(id));

            if (userLikesPost == null)
            {
                return NotFound();
            }

            return PartialView(userLikesPost);
        }

        #region create 
        //// GET: Likes/Create
        //public IActionResult Create()
        //{
        //    ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Id");
        //    ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
        //    return View();
        //} 
        

        // POST: Likes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,PostId,IsLiked")] UserLikesPost userLikesPost)
        {
            int flag = 0;
            if (ModelState.IsValid)
            {
                userLikesPost.UserId = _userManager.GetUserId(User);
                var u = await _context.UserLikesPosts.FirstOrDefaultAsync(m => m.UserId == userLikesPost.UserId && m.PostId == userLikesPost.PostId && m.IsLiked == true);
                if (u == null) //Like
                {
                    var uu = await _context.UserLikesPosts.FirstOrDefaultAsync(m => m.UserId == userLikesPost.UserId && m.PostId == userLikesPost.PostId);
                    if (uu == null) //first like
                    {
                        userLikesPost.IsLiked = true;
                        _context.Add(userLikesPost);
                    }
                    else //was liked before
                    {
                        uu.IsLiked = true;
                    }
                }
                else //Dislike
                {
                    u.IsLiked = false;
                }
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
            }
            ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Id", userLikesPost.PostId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userLikesPost.UserId);
            //return View(userLikesPost);
            return RedirectToAction("Index", "Home", new { flag });
        }
        #endregion

        #region edit
        //// GET: Likes/Edit/5
        //public async Task<IActionResult> Edit(string id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var userLikesPost = await _context.UserLikesPosts.FindAsync(id);
        //    if (userLikesPost == null)
        //    {
        //        return NotFound();
        //    }
        //    ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Id", userLikesPost.PostId);
        //    ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userLikesPost.UserId);
        //    return View(userLikesPost);
        //}

        //// POST: Likes/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(string id, [Bind("UserId,PostId,IsLiked")] UserLikesPost userLikesPost)
        //{
        //    if (id != userLikesPost.UserId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(userLikesPost);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!UserLikesPostExists(userLikesPost.UserId))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    ViewData["PostId"] = new SelectList(_context.Posts, "Id", "Id", userLikesPost.PostId);
        //    ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", userLikesPost.UserId);
        //    return View(userLikesPost);
        //} 
        #endregion

        #region delete
        //// GET: Likes/Delete/5
        //public async Task<IActionResult> Delete(string id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var userLikesPost = await _context.UserLikesPosts
        //        .Include(u => u.Post)
        //        .Include(u => u.User)
        //        .FirstOrDefaultAsync(m => m.UserId == id);
        //    if (userLikesPost == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(userLikesPost);
        //}

        //// POST: Likes/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(string id)
        //{
        //    var userLikesPost = await _context.UserLikesPosts.FindAsync(id);
        //    _context.UserLikesPosts.Remove(userLikesPost);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //} 
        #endregion

        private bool UserLikesPostExists(string id)
        {
            return _context.UserLikesPosts.Any(e => e.UserId == id);
        }
    }
}
