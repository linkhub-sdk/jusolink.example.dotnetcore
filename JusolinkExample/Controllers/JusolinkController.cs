using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Jusolink;

namespace JusolinkExample.Controllers
{
    public class JusolinkController : Controller
    {
        private readonly JusolinkService _jusolinkService;

        public JusolinkController(JusoInstance JusoInstance)
        {
            // 주소링크서비스 객체 생성
            _jusolinkService = JusoInstance.jusolinkService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Search()
        {
            return View("Search");
        }

        [HttpGet()]
        public IActionResult Zipcode(string IndexWord, int? PageNum)
        {
            
            if (IndexWord != null && IndexWord != "")
            {

                int? perPage = 20;
                bool noSuggest = false;
                bool noDiff = false;
                try
                {
                    SearchResult result = _jusolinkService.search(IndexWord, PageNum, perPage, noSuggest, noDiff);

                    return View("Zipcode", result);
                }
                catch (JusolinkException pe)
                {
                    return View("Exception", pe);
                }
            }

            return View("Zipcode");
        }

        /*
         * 주소API 이용 단가를 확인합니다.
         */
        public IActionResult GetUnitCost()
        {
            try
            {
                var result = _jusolinkService.GetUnitCost();
                return View("Result", result);
            }
            catch (JusolinkException pe)
            {
                return View("Exception", pe);
            }
        }

        /*
         * 연동회원 잔여포인트를 확인합니다.
         */
        public IActionResult GetBalance()
        {
            try
            {
                var result = _jusolinkService.GetBalance();
                return View("Result", result);
            }
            catch (JusolinkException pe)
            {
                return View("Exception", pe);
            }
        }
    }
}