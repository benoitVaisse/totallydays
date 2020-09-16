using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Totallydays.Models;
using Totallydays.Repositories;
using Totallydays.Services;

namespace Totallydays.Controllers.AdminControllers
{
    public class AdminEquipmentController : AdminController
    {
        private readonly EquipmentRepository _equipmentRepository;
        private readonly EquipmentTypeRepository _equipmentTypeRepo;

        public AdminEquipmentController(EquipmentRepository equipmentRepo, EquipmentTypeRepository equipmentTypeRepo)
        {
            this._equipmentRepository = equipmentRepo;
            this._equipmentTypeRepo = equipmentTypeRepo;
        }

        /// <summary>
        /// page admin des equipement 
        /// </summary>
        /// <returns></returns>
        [HttpGet("equipments", Name = "admin_equipments")]
        public IActionResult Equipments()
        {
            this.ViewBag.equipments_list = this._equipmentRepository.FindAll();
            this.ViewBag.equipments_type_list = this._equipmentTypeRepo.FindAll();
            Equipment Equipment = new Equipment();
            this.ViewBag.equipmentType = new Equipment_type();


            return View(Equipment);
        }


        /// <summary>
        /// create de type d'equipement
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("admin/create/equipment-type", Name ="admin_create_equipment_type_post")]
        public IActionResult CreateEquipmentType(Equipment_type model)
        {
            if(model.Equipment_type_id != 0)
            {
                model = this._equipmentTypeRepo.Update(model);
            }
            else
            {
                model = this._equipmentTypeRepo.create(model);
            }

            IEnumerable<Equipment_type> EquipmentsTypes = this._equipmentTypeRepo.FindAll();
            string allEquipmentView = ControllerExtenstionService.RenderViewToStringAsync(this, "~/Views/AdminEquipment/partial/_list_type.cshtml", EquipmentsTypes).Result;

            return Json(new {view = allEquipmentView,  model});
        }


        /// <summary>
        /// selectionne un type d'equipement
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("admin/edit/equipment-type", Name = "admin_edit_equipment_type_post")]
        public IActionResult EditEquipmentType(int id)
        {
            Equipment_type model = this._equipmentTypeRepo.FindOne(id);
            string status = "success";
            if(model == null) {
                status = "fail";
            }
            return Json(new { model, status });
        }


        /// <summary>
        /// creer ou update un equipoement
        /// </summary>
        /// <param name="Equipment"></param>
        /// <returns></returns>
        [HttpPost("admin/create/equipment" ,Name = "admin_create_equipment_post")]
        public IActionResult CreateUpdateEquipment(Equipment Equipment)
        {
            if(Equipment.Equipment_id != 0)
            {
                Equipment = this._equipmentRepository.Update(Equipment);
            }
            else
            {
                Equipment = this._equipmentRepository.Create(Equipment);
            }
            IEnumerable<Equipment_type> EquipmentsTypes = this._equipmentTypeRepo.FindAll();
            string view = ControllerExtenstionService.RenderViewToStringAsync(this, "~/Views/AdminEquipment/partial/_list_equipment.cshtml", EquipmentsTypes).Result;

            return Json(new { Equipment, view});
        }

        /// <summary>
        /// selectionne un equipement
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("admin/edit/equipment", Name = "admin_edit_equipment_post")]
        public IActionResult EditEquipment(int id)
        {
            Equipment Equipment = this._equipmentRepository.FindOneById(id);
            string status = "success";
            if (Equipment == null)
            {
                status = "fail";
            }
            return Json(new { Equipment, status });
        }
    }


}