using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ThucTapTaiTruong.Models.Entities;
using System.Data.Entity;
using System.Security.Cryptography;

namespace ThucTapTaiTruong.Controllers
{
    public class FurnitureController : Controller
    {
        private MyDBContext db = new MyDBContext();
        // GET: Furniture
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult RenderMain()
        {
            
            List<SanPham> listSanPham = db.SanPhams.Include(c => c.Anhs).ToList();
            ViewBag.MaTheLoai = db.TheLoais.Where(h => h.TrangThai == true).Select(x => x.MaTheLoai).ToList();
            ViewBag.Theloai = db.TheLoais.Where(h => h.TrangThai == true).Select(x => x.TenTheLoai).ToList();
            return PartialView("Main_Layout", listSanPham);
        }     
        
        public ActionResult RenderGridProduct()
        {
            return View();
        }

        public ActionResult gridproduct()
        {
            List<SanPham> listSanPham = db.SanPhams.Where(h => h.Xoa == true).Include(c => c.Anhs).ToList();
            ViewBag.MaTheLoai = db.TheLoais.Where(h => h.TrangThai == true).Select(x => x.MaTheLoai).ToList();
            ViewBag.Theloai = db.TheLoais.Where(h => h.TrangThai == true).Select(x => x.TenTheLoai).ToList();           
            return PartialView("grid-product", listSanPham);
        }
        public ActionResult Search(string key = "")
        {
            ViewBag.Key = key;
            var listSearch = db.SanPhams.Where(sp => sp.TenSP.Contains(key) == true).ToList();
            ViewBag.MaTheLoai = db.TheLoais.Where(h => h.TrangThai == true).Select(x => x.MaTheLoai).ToList();
            ViewBag.Theloai = db.TheLoais.Where(h => h.TrangThai == true).Select(x => x.TenTheLoai).ToList();
            return View(listSearch);
        }
        public ActionResult RenderGridProductByCatId()
        {
            return View();
        }
        public ActionResult GridProductByCatId(int id)
        {
            List<SanPham> listSanPham = db.SanPhams.Where(h => h.Xoa == true).Where(h => h.MaTheLoai == id).Include(c => c.Anhs).ToList();
            ViewBag.MaTheLoai = db.TheLoais.Where(h => h.TrangThai == true).Select(x => x.MaTheLoai).ToList();
            ViewBag.Theloai = db.TheLoais.Where(h => h.TrangThai == true).Select(x => x.TenTheLoai).ToList();
            return PartialView("grid-product", listSanPham);
        }
        public ActionResult RenderListProduct()
        {
            return View();
        }

        public ActionResult listproduct()
        {
            return PartialView("list-product");
        }

        public ActionResult RenderBlogGrid()
        {
            return View();
        }

        public ActionResult blogGrid()
        {
            return PartialView("blog-grid");
        }

        public ActionResult RenderBlogSingle()
        {
            return View();
        }

        public ActionResult blogSingle()
        {
            return PartialView("blog-single");
        }

        public ActionResult RenderCheckOut()
        {
            return View();
        }

        public ActionResult checkOut()
        {
            return PartialView("checkout");
        }

        public ActionResult RenderShoppingCart()
        {
            return View();
        }

        public ActionResult shoppingCart()
        {
            
            if (Session["idUser"] == null)
            {
                return PartialView("login");
            }
            else
            {
                int userID = (int)Session["idUser"];
                List<GioHang> listGioHang = db.GioHangs.Where(h => h.MaND == userID).Include(c => c.SanPham).ToList();
            
                return PartialView("shopping-cart", listGioHang);
            }
            
        }

        public ActionResult RenderAboutUs()
        {
            return View();
        }

        public ActionResult aboutUs()
        {
            return PartialView("about-us");
        }

        public ActionResult RenderContactUs()
        {
            return View();
        }

        public ActionResult contactUs()
        {
            return PartialView("contact-us");
        }

        public ActionResult RenderDetail()
        {
            return View();
        }

        public ActionResult Detail(int id)
        {
           SanPham sanPham = db.SanPhams.Where(h => h.MaSanPham == id).First();
           return PartialView("detail", sanPham);
        }

        public ActionResult RenderLogin()
        {
            return View();
        }

        public ActionResult Login()
        {     
            return PartialView("login");
        }

        //POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserRegister(NguoiDung _user)
        {
            if (ModelState.IsValid)
            {
                var check = db.NguoiDungs.FirstOrDefault(s => s.Email == _user.Email);
                if (check == null)
                {
                    db.Configuration.ValidateOnSaveEnabled = false;
                    _user.Password = Encode(_user.Password);
                    _user.MaChucVu = 2;
                    db.NguoiDungs.Add(_user);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["error"] = "Email already exists";
                    return RedirectToAction("RenderLogin");
                }


            }
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserLogin(string email, string password)
        {
            if (ModelState.IsValid)
            {

                var f_password = Encode(password);
                var data = db.NguoiDungs.Where(s => s.MaChucVu != 1).Where(s => s.Email.Equals(email) && s.Password.Equals(f_password)).Include(x =>x.GioHangs).ToList();               
                if (data.Count() > 0)
                {
                    //add session
                    Session["FullName"] = data.FirstOrDefault().HoTenND;
                    Session["Email"] = data.FirstOrDefault().Email;
                    Session["idUser"] = data.FirstOrDefault().MaND;
                    Session["SoHang"] = data.FirstOrDefault().GioHangs.Count();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Login failed";
                    return RedirectToAction("RenderLogin");
                }
            }
            return View();
        }
       

        //Logout
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("RenderLogin");
        }
        public ActionResult CartAdd(int productID, int SL)
        {

            if (Session["idUser"] == null)
            {
                return RedirectToAction("login");
            }
            int userID = (int)Session["idUser"];
            
            var _cart = new GioHang
            {
                MaSanPham = productID,
                MaND = userID
            };
            var check = db.GioHangs.FirstOrDefault(s => s.MaND == _cart.MaND && s.MaSanPham == _cart.MaSanPham);
            if (check == null)
            {
                db.Configuration.ValidateOnSaveEnabled = false;
                _cart.SoLuongTrongGio = SL;
                db.GioHangs.Add(_cart);
                db.SaveChanges();
                return View("RenderShoppingCart", check);
            }
            else
            {
                db.Configuration.ValidateOnSaveEnabled = false;
                int? _quantity = db.GioHangs.FirstOrDefault(s => s.MaND == _cart.MaND && s.MaSanPham == _cart.MaSanPham).SoLuongTrongGio;
                check.SoLuongTrongGio = (_quantity + SL);
                db.SaveChanges();
                return View("RenderShoppingCart",check);
            }
        }

        public ActionResult CartMinus(int productId)
        {
            int userID = (int)Session["idUser"]; 
            var _cart = new GioHang
            {
                MaSanPham = productId,
                MaND = userID
            };
            var check = db.GioHangs.FirstOrDefault(s => s.MaND == _cart.MaND && s.MaSanPham == _cart.MaSanPham);
            if (check == null)
            {
                return View("Index");
            }
            else if (check.SoLuongTrongGio <= 1)
            {
                db.Configuration.ValidateOnSaveEnabled = false;
                db.GioHangs.Remove(check);
                db.SaveChanges();
                return View("RenderShoppingCart", check);
            }
            else
            {
                db.Configuration.ValidateOnSaveEnabled = false;
                int? _quantity = db.GioHangs.FirstOrDefault(s => s.MaND == _cart.MaND && s.MaSanPham == _cart.MaSanPham).SoLuongTrongGio;
                check.SoLuongTrongGio = (_quantity - 1);
                db.SaveChanges();
                return View("RenderShoppingCart", check);
            }
        }
       

        public string Encode(string s)
        {
            string str = "";
            Byte[] buffer = System.Text.Encoding.UTF8.GetBytes(s);
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            buffer = md5.ComputeHash(buffer);
            foreach (Byte b in buffer)
            {
                str += b.ToString("X2");
            }
            return str;
        }

        

    }
}