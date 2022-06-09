using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicMvcApp.Data;
using MusicMvcApp.Models;

namespace MusicMvcApp.Controllers
{
    public class MusicController : Controller

        // Database contextinin Dependency Injection'ı
    {
        private readonly MusicContext _db;

        public MusicController(MusicContext db)
        {
            _db = db;
        }

        //Index sayfasına Musics Modelinin gönderilmesi
        public IActionResult Index()
        {
            IEnumerable<Music> objMusicList = _db.Musics;
            return View(objMusicList);
        }
        //Create Viewının gönderilmesi
        public IActionResult Create()
        {
            return View();
        }
        
        //AntiForgeryToken ve HttpPost request için annotationlar
        [ValidateAntiForgeryToken]
        [HttpPost]

        //Ajax request kullanıldığı için asenkron fonksiyonlar 
        public async Task<IActionResult> Create(Music obj)
        {
            if (ModelState.IsValid)
            {
                //Entity Framework kullanılarak objenin database'e eklenmesi
                _db.Musics.Add(obj);
                //Değişikliklerin asenkron olarak kaydedilmesi
                await (_db.SaveChangesAsync());
                //Index sayfasındaki geçici yazının geçici olarak değişmesi için tempdata bileşenin kullanılması
                TempData["success"] = "Şarkı başarıyla düzenlendi";
                //Index sayfasına redirect işlemi
                return RedirectToAction("Index");
                return View(obj);
                
            }
            return Json("Request Failed");
        }
        //GET

        //Güncelleme yapmak için idnin parametre olarak alınması
        public IActionResult Edit(int? id)
        {
            //Eğer parametredeki id databasede yok ise notfound sayfasının döndürülmesi
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var musicEntity = _db.Musics.Find(id);
        

            if (musicEntity == null)
            {
                return NotFound();
            }

            return View(musicEntity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        //Asenkron olarak düzenleme işlemi
        public async Task <IActionResult> Edit(Music obj)
        {
            
            if (!ModelState.IsValid) return BadRequest("Enter required fields");
                //Db context kullanılarak güncelleme işlemi
                _db.Musics.Update(obj);


            bool saveFailed;
            do
            {
                //Exception Handling
                saveFailed = false;

                try
                {
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    saveFailed = true;

                    
                    ex.Entries.Single().Reload();
                }

            } while (saveFailed);

            //Tempdatanın değiştirilmesi
            TempData["success"] = "Şarkı başarıyla düzenlendi";
                return RedirectToAction("Index");
                return this.Ok("Form Data received!");
                return View(obj);
        }
        //Silme işlemi yapmak için idnin parametre olarak alınması
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            
            var musicEntity = _db.Musics.FirstOrDefault(u=>u.Id==id);
            //var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);

            if (musicEntity == null)
            {
                return NotFound();
            }
            //Entitynin view a gönderilmesi
            return View(musicEntity);
        }

        //POST
        //DeletePOST fonksiyonun actionname annotationu sayesinde "Delete" olarak da fonksiyon görmesi sağlanıyor 
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> DeletePOST(int? id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Enter required fields");
            }

            //id kullanılarak objenin databaseden bulunması
            Music deletemusic = await (_db.Musics.FindAsync(id));
            //Objenin databaseden silinmesi
            _db.Musics.Remove(deletemusic);
            //Asenkron olarak database'in kaydedilmesi
            bool saveFailed;
            //Exception Handling
            do
            {
                saveFailed = false;

                try
                {
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    saveFailed = true;

                    
                    ex.Entries.Single().Reload();
                }

            } while (saveFailed);
            TempData["success"] = "Şarkı başarıyla silindi";
            return RedirectToAction("Index");

        }
    }

}

