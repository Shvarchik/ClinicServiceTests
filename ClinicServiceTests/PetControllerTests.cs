using ClinicService.Controllers;
using ClinicService.Models;
using ClinicService.Models.Requests;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicServiceTests
{
    /// <summary>
    /// ДОМАШНЯЯ РАБОТА: разработать методы тестирования контроллера PetController:
    /// Обновление данных UpdateTest
    /// Получение данных (по всем животным) GetAllTest
    /// Получение данных (по конкретному животному) GetByIdTest
    /// </summary>
    public class PetControllerTests
    {
        private PetController _petController;

        public PetControllerTests()
        {
            _petController = new PetController();
        }


        [Fact]
        public void PetCreateTest()
        {
            // МЕТОД ТЕСТИРОВАНИЯ СТОСТОИТ ИЗ 3 ЧАСТЕЙ

            // [1] Подготовка данных для тестирования
            CreatePetRequest createPetRequest = new CreatePetRequest();
            createPetRequest.Name = "Персик";
            createPetRequest.Birthday = DateTime.Now.AddYears(-15);
            createPetRequest.ClientId = 1;


            // [2] Исполнение тестируемой подпрограммы
            ActionResult<int> operationResult = _petController.Create(createPetRequest);

            // [3] Подготовка эталонного результата (expected), проверка результата
            int expectedOperationValue = 1;

            // Assert
            Assert.IsType<OkObjectResult>(operationResult.Result);
            Assert.Equal<int>(expectedOperationValue, (int)(((OkObjectResult)operationResult.Result).Value));

            
        }

        [Fact]
        public void PetCreateBadRequestTest()
        {
            CreatePetRequest createPetRequest = new CreatePetRequest();
            createPetRequest.Name = "к";
            createPetRequest.Birthday = DateTime.Now.AddYears(-55);
            createPetRequest.ClientId = 0;

            ActionResult<int> operationResult = _petController.Create(createPetRequest);

            int expectedOperationValue = 0;

            Assert.IsType<BadRequestObjectResult>(operationResult.Result);
            Assert.Equal<int>(expectedOperationValue, (int)(((BadRequestObjectResult)operationResult.Result).Value));
        
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-1000)]
        public void PetDeleteBadRequestTest(int petId)
        {
            // [2] Исполнение тестируемой подпрограммы
            ActionResult<int> operationResult = _petController.Delete(petId);

            // [3] Подготовка эталонного результата (expected), проверка результата
            int expectedOperationValue = 0;

            // Assert
            Assert.IsType<BadRequestObjectResult>(operationResult.Result);
            Assert.Equal<int>(expectedOperationValue, (int)(((BadRequestObjectResult)operationResult.Result).Value));
        }


        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(500)]
        public void PetDeleteTest(int petId)
        {
            // [2] Исполнение тестируемой подпрограммы
            ActionResult<int> operationResult = _petController.Delete(petId);

            // [3] Подготовка эталонного результата (expected), проверка результата
            int expectedOperationValue = 1;

            // Assert
            Assert.IsType<OkObjectResult>(operationResult.Result);
            Assert.Equal<int>(expectedOperationValue, (int)(((OkObjectResult)operationResult.Result).Value));
        }

        [Fact]
        public void PetUpdateTest()
        {
            // [1] Подготовка данных для тестирования
            UpdatePetRequest updatePetRequest = new UpdatePetRequest();
            updatePetRequest.Name = "Персик";
            updatePetRequest.Birthday = DateTime.Now.AddYears(-15);
            updatePetRequest.PetId = 1;


            // [2] Исполнение тестируемой подпрограммы
            ActionResult<int> operationResult = _petController.Update(updatePetRequest);

            // [3] Подготовка эталонного результата (expected), проверка результата
            int expectedOperationValue = 1;

            // Assert
            Assert.IsType<OkObjectResult>(operationResult.Result);
            Assert.Equal<int>(expectedOperationValue, (int)(((OkObjectResult)operationResult.Result).Value));
        }

        [Fact]
        public void PetUpdateBadRequestTest()
        {
            UpdatePetRequest updatePetRequest = new UpdatePetRequest();
            updatePetRequest.Name = "П";
            updatePetRequest.Birthday = DateTime.Now.AddYears(-30);
            updatePetRequest.PetId = 1;

            ActionResult<int> operationResult = _petController.Update(updatePetRequest);

            int expectedOperationValue = 0;

            Assert.IsType<BadRequestObjectResult>(operationResult.Result);
            Assert.Equal<int>(expectedOperationValue, (int)(((BadRequestObjectResult)operationResult.Result).Value));

        }

        [Fact]
        public void PetUpdateNotFoundTest() 
        {
            UpdatePetRequest updatePetRequest = new UpdatePetRequest();
            updatePetRequest.Name = "П";
            updatePetRequest.Birthday = DateTime.Now.AddYears(-30);
            updatePetRequest.PetId = 0;

            ActionResult<int> operationResult = _petController.Update(updatePetRequest);

            int expectedOperationValue = 0;
            Assert.IsType<NotFoundObjectResult>(operationResult.Result);
            Assert.Equal<int>(expectedOperationValue, (int)(((NotFoundObjectResult)operationResult.Result).Value));
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(50)]
        public void PetGetAllTest(int clientId)
        {
            var operationResult = _petController.GetAll(clientId);
            
            Assert.IsType<OkObjectResult>(operationResult.Result);
            Assert.IsAssignableFrom<List<Pet>>(((OkObjectResult)operationResult.Result).Value);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(-50)]
        public void PetGetAllBadRequestTest(int clientId)
        {
            var operationResult = _petController.GetAll(clientId);

            Assert.IsType<BadRequestObjectResult>(operationResult.Result);
            Assert.IsAssignableFrom<int>(((BadRequestObjectResult)operationResult.Result).Value);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(10)]
        [InlineData(50)]
        public void PetGetByIdTest(int petId)
        {
            var operationResult = _petController.GetById(petId);

            Assert.IsType<OkObjectResult>(operationResult.Result);
            Assert.IsAssignableFrom<Pet>(((OkObjectResult)operationResult.Result).Value);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-5)]
        public void PetGetByIdBadRequestTest(int petId)
        {
            var operationResult = _petController.GetById(petId);

            Assert.IsType<BadRequestObjectResult>(operationResult.Result);
            Assert.IsAssignableFrom<int>(((BadRequestObjectResult)operationResult.Result).Value);

        }
    }
}
