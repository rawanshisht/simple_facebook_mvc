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
    public class PostsController : Controller
    {
        private readonly IHostingEnvironment _he;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;


        List<Post> posts = new List<Post>();
        public PostsController(ApplicationDbContext context, IHostingEnvironment he, UserManager<User> UserManager, SignInManager<User> SignInManager)
        {
            _context = context;
            _he = he;
            _userManager = UserManager;
            _signInManager = SignInManager;
            //List<Post> posts = new List<Post>();
        }

        #region commented
        //// GET: Posts
        //public async Task<IActionResult> Index()
        //{
        //    var applicationDbContext = _context.Posts.Include(p => p.User);
        //    return View(await applicationDbContext.ToListAsync());
        //}

        //// GET: Posts/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var post = await _context.Posts
        //        .Include(p => p.User)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (post == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(post);
        //}

        //// GET: Posts/Create
        //public IActionResult Create()
        //{
        //    ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
        //    return View();
        //}
        #endregion

        #region Create 
        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Content,PostingDate,IsDeleted,UserId")] Post post, IFormFile pic)
        {
            //posts.Add(post);
            int flag = 0;
            if (ModelState.IsValid && post.Content != null)
            {
                flag = 0;
                post.UserId = _userManager.GetUserId(User);
                post.PostingDate = DateTime.Now;
                _context.Add(post);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                //return View(posts);
            }
            else if (pic != null)
            {
                flag = 1;

                var picName = Path.GetFileName(pic.FileName);
                var picLocation = Path.Combine(_he.WebRootPath, picName);
                pic.CopyTo(new FileStream(picLocation, FileMode.Create));


                //Other code
                //using (var stream = System.IO.File.Open(@"D:\ITI\ASP.NET MVC\Project\MVCProject\FacebookApp\wwwroot", FileMode.Open))
                //{
                //    // Use stream
                //}

                var picPath = "/" + Path.GetFileName(pic.FileName);
                post.UserId = _userManager.GetUserId(User);
                post.PostingDate = DateTime.Now;
                post.Content = picPath;
                _context.Add(post);
                await _context.SaveChangesAsync();
            }

            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", post.UserId);
            //return RedirectToAction("Index", "Timeline", new { flag });
            return RedirectToAction("Index", "Home", new { flag });
        }

        #endregion

        #region Edit
        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", post.UserId);
            return PartialView(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Content,PostingDate,IsDeleted,UserId")] Post post)
        {
            //if (id != post.Id)
            //{
            //    return NotFound();
            //}

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                //return RedirectToAction(actionName:"Index",controllerName:"Timeline");
                return RedirectToAction(actionName: "Index", controllerName: "Home");
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", post.UserId);
            //return RedirectToAction(actionName: "Index", controllerName: "Timeline");
            return RedirectToAction(actionName: "Index", controllerName: "Home");
        }

        #endregion

        #region Delete
        // GET: Posts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            #region for alert
            //if (id == null)
            //{
            //    return NotFound();
            //}

            ////var post = await _context.Posts
            ////    .Include(p => p.User)
            ////    .FirstOrDefaultAsync(m => m.Id == id);
            //var post = await _context.Posts.FindAsync(id);

            //if (post == null)
            //{
            //    return NotFound();
            //}

            ////return View(post);
            //post.IsDeleted = true;
            ////_context.Posts.Remove(post);
            //await _context.SaveChangesAsync();
            ////return RedirectToAction("Index", "Timeline");
            //return RedirectToAction("Index", "Home");
            #endregion

            if (id == null)
            {
                return BadRequest();
            }
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return PartialView(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            post.IsDeleted = true;
            //_context.Posts.Remove(post);
            await _context.SaveChangesAsync();
            //return RedirectToAction(actionName: "Index", controllerName: "Timeline");
            return RedirectToAction(actionName: "Index", controllerName: "Home");
        }

        #endregion

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }
    }
}
