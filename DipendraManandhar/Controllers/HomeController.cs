using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DipendraManandhar.Models;
using DipendraManandhar.ViewModel;

namespace DipendraManandhar.Controllers
{
    public class HomeController : Controller
    {
        private intervnimageEntities db = new intervnimageEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult InterviewIndex()
        {
            InterviewIndexVM Vm = new InterviewIndexVM();
            Vm.indexlist = db.Interviews.ToList() ;
            Vm.droplist = db.InterviewDrops.ToList();
            Vm.CountryList = db.Countries.ToList();
            Vm.StateList = db.States.ToList();
            return View(Vm);
        }
        //public ActionResult InterviewAdd()
        //{
        //    try
        //    {
        //        InterviewAddEdit Vm = new InterviewAddEdit();
        //        Vm.Droplist = db.InterviewDrops.ToList();
        //        return View(Vm);
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewBag.message = ex.Message;
        //        return View(new InterviewAddEdit());
        //    }
        //}

        public ActionResult InterviewAdd(int Id)
        {
            try
            {
                InterviewAddEdit Vm = new InterviewAddEdit();
                Vm.Droplist = db.InterviewDrops.ToList();
                Vm.CountryList = db.Countries.ToList();
                Vm.StateList = db.States.ToList();

                if (Id == 0)
                    return View(Vm);
                else
                {
                    Vm.InterviewObj = db.Interviews.Where(x => x.Id == Id).FirstOrDefault();
                    return View(Vm);
                }
            }
            catch(Exception ex)
            {
                ViewBag.message = ex.Message;
                return View(new InterviewAddEdit());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InterviewAdd(InterviewAddEdit Submit, HttpPostedFileBase ImageData)
        {
            Submit.Droplist = db.InterviewDrops.ToList();
            Submit.CountryList = db.Countries.ToList();
            Submit.StateList = db.States.ToList();            
            try
            {
                if (ImageData != null)
                {
                    if (!ModelState.IsValid)
                    {
                        return View(Submit);
                    }
                    //....
                    //using (FileStream fs = new FileStream(pathToCheck, FileMode.Create))
                    //{
                    //    using (BinaryWriter bw = new BinaryWriter(fs))
                    //    {
                    //        byte[] data = Convert.FromBase64String(ImageString);
                    //        bw.Write(data);
                    //        bw.Close();
                    //    }
                    //    fs.Close();
                    //}
                    //...

                    // Specify the path to save the uploaded file to.
                    // Get the name of the file to upload.
                    else
                    {
                        using (var dbContextTransaction = db.Database.BeginTransaction())
                        {
                            try
                            {
                                var supportedTypes = new[] { "JPG", "BMP", "PDF", "PNG", "GIF", "JPEG" };
                                var fileExt = System.IO.Path.GetExtension(ImageData.FileName).Substring(1);
                                fileExt = fileExt.ToUpper();
                                if (!supportedTypes.Contains(fileExt))
                                {
                                    ViewBag.Status = "File Extension Is InValid - Only Upload JPG/PDF/PNG/GIF/JPEG File";
                                    return View(Submit);
                                }
                                else
                                {
                                    //5digit counter starting
                                    Random rand = new Random();
                                    int count = 0;
                                    string counter = "";
                                    count = rand.Next(0, 100000);
                                    counter = count.ToString("d5");

                                    string fileName = counter + System.IO.Path.GetFileName(ImageData.FileName);
                                    //.
                                    string pathToCheck = System.IO.Path.Combine(
                                                           Server.MapPath("~/Image"), fileName);
                                    string tempfileName = "";
                                    if (System.IO.File.Exists(pathToCheck))
                                    {

                                        while (System.IO.File.Exists(pathToCheck))
                                        {
                                            //5-digit prefix int in filename
                                            count = rand.Next(0, 100000);
                                            counter = count.ToString("d5");

                                            string FileNameWithoutExt = System.IO.Path.GetFileNameWithoutExtension(fileName);
                                            // if a file with this name already exists,
                                            // prefix the filename with a number.
                                            tempfileName = counter + FileNameWithoutExt + "." + fileExt;
                                            //pathToCheck = savePath + tempfileName;
                                            pathToCheck = System.IO.Path.Combine(
                                                           Server.MapPath("~/Image"), tempfileName);
                                        }
                                        pathToCheck = tempfileName;
                                        fileName = tempfileName;

                                        // Notify the user that the file name was changed.
                                        ViewBag.Status = "A file with the same name already exists." +
                                            "<br />Your file was saved as " + fileName;
                                    }
                                    else
                                    {
                                        // file was saved.
                                        ViewBag.Status = "Your file was uploaded successfully.";
                                    }
                                    //extra step. for confirmation
                                    pathToCheck = System.IO.Path.Combine(
                                                       Server.MapPath("~/Image"), fileName);
                                    if (Submit.InterviewObj.Id == 0)
                                    {
                                        Submit.InterviewObj.Photolink = fileName;
                                        db.Interviews.Add(Submit.InterviewObj);
                                        db.SaveChanges();
                                        ViewBag.message = "added sucessfully";
                                    }
                                    else
                                    {
                                        var dbobj = db.Interviews.Where(x => x.Id == Submit.InterviewObj.Id).FirstOrDefault();
                                        string PhotoName = dbobj.Photolink;
                                        if (PhotoName != null)
                                        {
                                            string pathToDel = System.IO.Path.Combine(
                                                                       Server.MapPath("~/Image"), PhotoName);
                                            if (System.IO.File.Exists(pathToDel))
                                            {
                                                System.IO.File.Delete(pathToDel);
                                                //Session["DeleteSuccess"] = "Yes";
                                            }
                                        }
                                        Submit.InterviewObj.Photolink = fileName;
                                        db.Entry(dbobj).CurrentValues.SetValues(Submit.InterviewObj);

                                        //db.Interviews.Attach(Submit.InterviewObj);
                                        //var entry = db.Entry(Submit.InterviewObj);
                                        //entry.Property(e => e.Id).IsModified = true;
                                        //entry.Property(e => e.FirstName).IsModified = true;
                                        //entry.Property(e => e.LastName).IsModified = true;
                                        //entry.Property(e => e.Message).IsModified = true;
                                        //entry.Property(e => e.DropDown).IsModified = true;
                                        //entry.Property(e => e.Email).IsModified = true;
                                        //entry.Property(e => e.MobileNo).IsModified = true;
                                        //entry.Property(e => e.Hobby).IsModified = true;
                                        //entry.Property(e => e.Photolink).IsModified = true;
                                        //entry.Property(e => e.State).IsModified = true;
                                        //entry.Property(e => e.Country).IsModified = true;
                                        db.SaveChanges();
                                        ViewBag.message = "modified sucessfully";
                                    }
                                    //.
                                    ImageData.SaveAs(pathToCheck);
                                    dbContextTransaction.Commit();
                                    return RedirectToAction("InterviewIndex");
                                    //incase of converted or manipulated data send as string ImageString in parameter
                                    //using (FileStream fs = new FileStream(pathToCheck, FileMode.Create))
                                    //{
                                    //    using (BinaryWriter bw = new BinaryWriter(fs))
                                    //    {
                                    //        byte[] data = Convert.FromBase64String(ImageString);
                                    //        bw.Write(data);
                                    //        bw.Close();
                                    //    }
                                    //    fs.Close();
                                    //}

                                }
                            }
                            catch (Exception)
                            {
                                dbContextTransaction.Rollback();
                                ViewBag.Status = "Error in adding or modifying";
                                return View(Submit);
                            }
                        }
                    }
                    
                }
                else
                {
                    ViewBag.Status = "File not selected";
                    return View(Submit);
                }
            }
            catch (Exception ex) { ViewBag.message = ex.Message+"!!error occured!!"; return View(Submit); }
        }
        //for delete posting garne ho but for fast hope u understand
        //public ActionResult delete(int id)
        //{
        //    if (id == 0)
        //    {
        //        return RedirectToAction("InterviewIndex");
        //    }
        //    try
        //    {
        //        Interview del = db.Interviews.Where(x => x.Id == id).FirstOrDefault();
        //        db.Interviews.Remove(del);
        //        db.SaveChanges();
        //        ViewBag.message = "Successfully deleted";
        //        return RedirectToAction("InterviewIndex");
        //    }
        //    catch (Exception Ex)
        //    {
        //        ViewBag.message = Ex.Message;
        //        return RedirectToAction("InterviewIndex");
        //    }
        //}
        [HttpPost]
        public ActionResult delete(FormCollection Id)
        {
            int id=0;
            int.TryParse(Id["id"],out id);
            if (id == 0)
            {
                return RedirectToAction("InterviewIndex");
            }
            try
            {
                Interview del = db.Interviews.Where(x => x.Id == id).FirstOrDefault();
                string PhotoName = del.Photolink;
                if (PhotoName != null)
                {
                    string pathToDel = System.IO.Path.Combine(
                                               Server.MapPath("~/Image"), PhotoName);
                    if (System.IO.File.Exists(pathToDel))
                    {
                        System.IO.File.Delete(pathToDel);
                        //Session["DeleteSuccess"] = "Yes";
                    }
                }
                db.Interviews.Remove(del);
                db.SaveChanges();
                ViewBag.message = "Successfully deleted";
                return RedirectToAction("InterviewIndex");
            }
            catch (Exception Ex)
            {
                ViewBag.message = Ex.Message;
                return RedirectToAction("InterviewIndex");
            }
        }
        public ActionResult ViewInterview(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("InterviewIndex");
            }
            try
            {
                InterviewAddEdit Submit=new InterviewAddEdit();
                Interview del = db.Interviews.Where(x => x.Id == id).FirstOrDefault();
                Submit.InterviewObj = del;
                Submit.Droplist = db.InterviewDrops.ToList();
                Submit.CountryList = db.Countries.ToList();
                Submit.StateList = db.States.ToList();
                return View(Submit);
            }
            catch (Exception Ex)
            {
                ViewBag.message = Ex.Message;
                return RedirectToAction("InterviewIndex");
            }
        }
        [HttpGet]
        public ActionResult ForImageNCascading(int CountryId)
        {
            if (CountryId == 0)
            {
                return null;
            }
            else
            {
                var ret= db.States.Where(x => x.CountryId == CountryId).ToList();
                List<SelectListItem> abc = new List<SelectListItem>();
                foreach (var item in ret)
                {
                    SelectListItem add = new SelectListItem();
                    add.Text =item.StateName;
                    add.Value =item.Id.ToString();
                    abc.Add(add);
                }
                return Json(abc, JsonRequestBehavior.AllowGet);
            }
        }
    }
}