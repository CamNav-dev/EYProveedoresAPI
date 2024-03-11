using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EYProveedores.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Xml;
using System.Reflection.Metadata;
namespace EYProveedores.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuentesHighRisk : ControllerBase
    {
        private ChromeDriver _driver = new();

        // GET: api/<HighRiskController>/world-bank
        [HttpGet("world-bank")]
        public IEnumerable<TheWorldBankSource> Get()
        {
            _driver.Url = "https://projects.worldbank.org/en/projects-operations/procurement/debarred-firms";

            // wait for the page to load k-grid-content
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.FindElement(By.ClassName("k-grid-content")));

            // div have class k-grid-content
            var div = _driver.FindElement(By.ClassName("k-grid-content"));

            var table = div.FindElement(By.TagName("table"));

            var sources = new System.Collections.Generic.List<TheWorldBankSource>();

            var rows = table.FindElements(By.TagName("tr"));

            foreach (var row in rows)
            {
                var cells = row.FindElements(By.TagName("td"));
                if (cells.Count >= 7)
                {
                    string firmName = cells[0].Text;
                    string address = cells[2].Text;
                    string country = cells[3].Text;
                    DateTime fromDate;
                    try
                    {
                        fromDate = DateTime.Parse(cells[4].Text);
                    }
                    catch (Exception e)
                    {
                        fromDate = DateTime.Now;
                    }

                    DateTime toDate;
                    try
                    {
                        toDate = DateTime.Parse(cells[5].Text);
                    }
                    catch (Exception e)
                    {
                        toDate = DateTime.Now;
                    }

                    string grounds = cells[6].Text;
                    var source = new TheWorldBankSource
                    {
                        FirmName = firmName,
                        Address = address,
                        Country = country,
                        FromDate = fromDate,
                        ToDate = toDate,
                        Grounds = grounds
                    };
                    sources.Add(source);

                }
            }

            _driver.Close();

            return sources;
        }

        // GET api/<HighRiskController>/offshore/{company}
        [HttpGet("offshore/{company}")]
        public IEnumerable<OffshoreSource> Get(string company)
        {
            // Navigate to the webpage
            _driver.Url = "https://offshoreleaks.icij.org/search?q=" + company;

            // Wait for the page to load
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            // wait till the element table-striped is loaded
            wait.Until(d => d.FindElement(By.ClassName("table-striped")));

            // Find the table
            IWebElement table = _driver.FindElement(By.ClassName("table-striped")).FindElement(By.TagName("tbody"));

            var results = new List<OffshoreSource>();

            if (table != null)
            {
                var rows = table.FindElements(By.TagName("tr"));
                foreach (var row in rows)
                {
                    var cols = row.FindElements(By.TagName("td"));
                    string entity = cols[0].FindElement(By.TagName("a")).GetAttribute("innerText");
                    string jurisdiction = cols[1].GetAttribute("innerText");
                    string linkedTo = cols[2].GetAttribute("innerText");
                    string dataFrom = cols[3].FindElement(By.TagName("a")).GetAttribute("href");

                    var source = new OffshoreSource
                    {
                        Entity = entity,
                        Jurisdiction = jurisdiction,
                        LinkedTo = linkedTo,
                        DataFrom = dataFrom
                    };

                    results.Add(source);
                }
            }

            _driver.Quit();

            return results;
        }

        // GET api/<HighRiskController>/ofac/{company}
        [HttpGet("ofac/{company}")]
        public IEnumerable<Offacsource> GetOfacSource(string company)
        {
            // Navigate to the webpage
            _driver.Url = "https://sanctionssearch.ofac.treas.gov/";

            // Wait for the page to load
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.FindElement(By.Id("ctl00_MainContent_pnlSearch")));

            // Find the search input
            IWebElement searchInput = _driver.FindElement(By.Id("ctl00_MainContent_txtLastName"));
            searchInput.SendKeys(company);

            // Find the search button
            IWebElement searchButton = _driver.FindElement(By.Id("ctl00_MainContent_btnSearch"));
            searchButton.Click();

            // Wait for the page to load
            wait.Until(d => d.FindElement(By.Id("gvSearchResults")));

            // Find the table
            IWebElement table = _driver.FindElement(By.Id("gvSearchResults"));

            var results = new List<Offacsource>();

            if (table != null)
            {
                var rows = table.FindElements(By.TagName("tr"));
                foreach (var row in rows)
                {
                    var cols = row.FindElements(By.TagName("td"));
                    string name = cols[0].GetAttribute("innerText");
                    string address = cols[1].GetAttribute("innerText");
                    string type = cols[2].GetAttribute("innerText");
                    string programs = cols[3].GetAttribute("innerText");
                    string list = cols[4].GetAttribute("innerText");
                    string score = cols[5].GetAttribute("innerText");
                    var source = new Offacsource
                    {
                        Name = name,
                        Address = address,
                        Type = type,
                        Programs = programs,
                        List = list,
                        Score = score
                    };

                    results.Add(source);
                }
            }

            _driver.Quit();

            return results;
        }


    }
}
