using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ReverseAStringMVCPractice.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ReverseAStringMVCPractice.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Reverse()
        {
            Palindrome model = new();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Reverse(Palindrome palindrome)
        {
            string inputWord = palindrome.InputWord;
            char[] charArray = inputWord.ToCharArray();
            Array.Reverse(charArray);
            string revWord = new string(charArray);
            palindrome.RevWord = revWord;
            inputWord = Regex.Replace(inputWord.ToLower(), "[^a-zA-Z0-9]+", "");
            revWord = Regex.Replace(revWord.ToLower(), "[^a-zA-Z0-9]+", "");

            if(revWord == inputWord)
            {
                palindrome.IsPalindrome = true;
                palindrome.Message = $"Succes {palindrome.InputWord} is a Palidrome";
            }
            else
            {
                palindrome.IsPalindrome = false;
                palindrome.Message = $"Failed {palindrome.InputWord} is not a Palidrome";
            }


            return View(palindrome);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
