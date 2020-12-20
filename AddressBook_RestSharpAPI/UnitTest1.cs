using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Collections.Generic;
using System.Net;

namespace AddressBook_RestSharpAPI
{
    [TestClass]
    public class UnitTest1
    {
        RestClient client;

        [TestInitialize]
        public void setUp()
        {
            client = new RestClient(" http://localhost:3000");
        }
        private IRestResponse getAddressBookList()
        {
            //arrange
            RestRequest request = new RestRequest("/addressBook", Method.GET);
            //act
            IRestResponse response = client.Execute(request);
            return response;
        }
        [TestMethod]
        public void whileCallingGETAPi_shouldReturnAddressBookList()
        {
            IRestResponse response = getAddressBookList();

            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            List<AddressBook> dataResponse = JsonConvert.DeserializeObject<List<AddressBook>>(response.Content);
            Assert.AreEqual(4, dataResponse.Count);
        }
        [TestMethod]
        public void whileAddingMultiplePersons_OnPost_ShouldRetuenAddedPersons()
        {
            List<AddressBook> addressBookList = new List<AddressBook>();
            addressBookList.Add(new AddressBook { Firstname = "Rishitha", Lastname = "Reddy", Address = "KLKY", City = "JCL", State = "UP", Zip = 509215, PhoneNumber = "9510247863" });
            addressBookList.Add(new AddressBook { Firstname = "Sri", Lastname = "Charitha", Address = "BPL", City = "HYD", State = "MP", Zip = 509213, PhoneNumber = "9510247123" });
            foreach(AddressBook addressBook in addressBookList)
            {
                RestRequest request = new RestRequest("/addressBook", Method.POST);
                JObject jObjectbody = new JObject();
                jObjectbody.Add("Firstname", addressBook.Firstname);
                jObjectbody.Add("Lastname", addressBook.Lastname);
                jObjectbody.Add("Address", addressBook.Address);
                jObjectbody.Add("City", addressBook.City);
                jObjectbody.Add("State", addressBook.State);
                jObjectbody.Add("Zip", addressBook.Zip);
                jObjectbody.Add("PhoneNumber", addressBook.PhoneNumber);


                request.AddParameter("application/json", jObjectbody, ParameterType.RequestBody);

                IRestResponse response = client.Execute(request);

                Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
                AddressBook dataResponse = JsonConvert.DeserializeObject<AddressBook>(response.Content);
                Assert.AreEqual(addressBook.Firstname, dataResponse.Firstname);
            }
        }
        [TestMethod]
        public void updatingTheperson_OnPUT_ShouldUpadateDetalis()
        {
            RestRequest request = new RestRequest("/addressBook/5", Method.PUT);
            JObject jObjectbody = new JObject();
            jObjectbody.Add("Firstname", "Sri");
            jObjectbody.Add("Lastname", "Charitha");
            jObjectbody.Add("Address", "BPL");
            jObjectbody.Add("City", "Bijinapally");
            jObjectbody.Add("State", "TM");
            jObjectbody.Add("Zip", 500012);
            jObjectbody.Add("PhoneNumber", "9510247123");

            request.AddParameter("application/json", jObjectbody, ParameterType.RequestBody);

            IRestResponse response = client.Execute(request);

            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            AddressBook dataResponse = JsonConvert.DeserializeObject<AddressBook>(response.Content);
            Assert.AreEqual("Bijinapally", dataResponse.City);
            Assert.AreEqual("TM", dataResponse.State);
        }
    }
}
