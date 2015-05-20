using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Services;
using Services.DTO;
using UI.WEB.ViewModel;
using StatusFlag = UI.WEB.ViewModel.StatusFlag;

namespace UI.WEB.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var hosts = HostFileOperation.GetAllHosts().OrderByDescending(x => x.HostStatus).ToList();

            if (hosts.Count() == 0) return View();
            var model = new List<HostVm>();
            var i = 1;
            foreach (var h in hosts)
            {
                var host = new HostVm
                {
                    AdminEmail = h.AdminEmail,
                    HostAddress = h.HostAddress,
                    HostId = i,
                    FrequencyRequest = h.FrequencyRequest,
                    IntervalRequest = h.IntervalRequest,
                    Status = (StatusFlag)h.HostStatus,
                    HostName = h.HostName
                };
                i++;
                model.Add(host);
            }

            return View(model);
        }

        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(HostVm model)
        {
            if (!ModelState.IsValid) return View(model);
            var newHost = new Host
            {
                HostName = model.HostName,
                AdminEmail = model.AdminEmail,
                HostAddress = model.HostAddress,
                FrequencyRequest = model.FrequencyRequest,
                HostStatus = Services.DTO.StatusFlag.Work,
                IntervalRequest = model.IntervalRequest
            };
            HostFileOperation.Create(newHost);
            return RedirectToAction("Index");
        }

        public ActionResult Edit(string nameHost)
        {
            var host = HostFileOperation.GetByNameHost(nameHost);
            if (host == null) return View();
            return View(new HostVm
            {
                HostName = host.HostName,
                AdminEmail = host.AdminEmail,
                FrequencyRequest = host.FrequencyRequest,
                HostAddress = host.HostAddress,
                Status = (StatusFlag)host.HostStatus,
                IntervalRequest = host.IntervalRequest
            });
        }

        [HttpPost]
        public ActionResult Edit(HostVm model, string oldHost)
        {
            if (!ModelState.IsValid) return View(model);
            var newHost = new Host
            {
                HostName = model.HostName,
                IntervalRequest = model.IntervalRequest,
                HostStatus = (Services.DTO.StatusFlag)model.Status,
                AdminEmail = model.AdminEmail,
                FrequencyRequest = model.FrequencyRequest,
                HostAddress = model.HostAddress
            };
            HostFileOperation.Update(newHost);
            return RedirectToAction("Index");
        }

        public ActionResult Del(string nameHost)
        {
            HostFileOperation.Delete(nameHost);
            return RedirectToAction("Index");
        }

        public ActionResult RefresStatus(string nameHost)
        {
            HostFileOperation.UpdateStatus(nameHost, Services.DTO.StatusFlag.Work);
            return RedirectToAction("Index");
        }
    }
}