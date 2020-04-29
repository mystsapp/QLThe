using CrystalDecisions.CrystalReports.Engine;
using CrystalMVCReport.Models;
using CrystalMVCReport.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNetCore.Mvc;

namespace CrystalMVCReport.Controllers
{
    public class ReportTestController : Microsoft.AspNetCore.Mvc.Controller
    {
        // GET: ReportTest
        public void Test1(string name)
        {
            Test(name);
        }
        public System.Web.Mvc.ActionResult Test(string name)
        {
            var listItem = new List<CapTheViewModel>()
            {
                new CapTheViewModel(){MaCapThe = "1", NguoiCap = "Nguyen Van A", MaCN = "001", NguoiNhan = "Nguyen Van B", TongSoLuong = 1, MayTinh = "PC1"},
                new CapTheViewModel(){MaCapThe = "2", NguoiCap = "Nguyen Van C", MaCN = "001", NguoiNhan = "Nguyen Van D", TongSoLuong = 10, MayTinh = "PC2"}
            };
            var items = JsonConvert.DeserializeObject<List<CapTheViewModel>>(name);
            var dt = EntityToTable.ToDataTable(items);
            //ReportDocument rd = new ReportDocument();
            //rd.Load(Path.Combine(Server.MapPath("~/Report"), "CrystalReport_CapThes.rpt"));
           // rd.Load("http://localhost:57708/CrystalMVCReport/Report/CrystalReport_CapThes.rpt");

            string reportPath = "http://localhost:57708/CrystalMVCReport/Report/CrystalReport_CapThes.rpt";
            return new CrystalReportPdfResult(reportPath, dt);
        }
    }
}