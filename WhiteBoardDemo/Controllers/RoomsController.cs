using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WhiteBoardDemo.Hubs;
using WhiteBoardDemo.Models;
using WhiteBoardDemo.Services;

namespace WhiteBoardDemo.Controllers
{
    [System.Web.Mvc.Authorize]
    public class RoomsController : Controller
    {

        private readonly IRoomsService _IRoomsService;

        public RoomsController()
        {
            _IRoomsService = new RoomsService(new ApplicationDbContext());
        }

        //private ApplicationDbContext db = new ApplicationDbContext();


        public ActionResult Index()
        {
            return View(_IRoomsService.GetAllRooms());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = _IRoomsService.GetRoomById(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            IEnumerable<Message> RoomMessages = _IRoomsService.GetRoomMessages(id);
            RoomViewModel roomvm = new RoomViewModel { Id = room.Id, RoomName = room.RoomName, Messages = RoomMessages };
            if (room.WhiteboardData != null)
            {
                var base64String = Convert.ToBase64String(room.WhiteboardData);
                var dataUrl = $"data:image/png;base64,{base64String}";
                roomvm.WhiteboardData = dataUrl;

            }

            return View(roomvm);
        }

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,RoomName")] Room room)
        {
            if (ModelState.IsValid)
            {
                var hubContext = GlobalHost.ConnectionManager.GetHubContext<WhiteboardHub>();
                _IRoomsService.CreateRoom(room);
                hubContext.Clients.All.createRoom(room);
                return RedirectToAction("Details", room);
            }

            return View(room);
        }

        public ActionResult Join(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = _IRoomsService.GetRoomById(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            _IRoomsService.AddRoomMember(room, HttpContext.User.Identity.Name);
            return RedirectToAction("Details", room);

        }
        public ActionResult Leave(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = _IRoomsService.GetRoomById(id);
            if (room == null)
            {
                return HttpNotFound();
            }

            _IRoomsService.LeaveRoom(id, HttpContext.User.Identity.Name);
            return RedirectToAction("Index");

        }



        // GET: Rooms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = _IRoomsService.GetRoomById(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }

        // POST: Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _IRoomsService.RemoveRoom(id);
            return RedirectToAction("Index");
        }

   
    }
}
