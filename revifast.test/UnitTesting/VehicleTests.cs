using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Revifast.Api.Controllers;
using Revifast.Api.Models.Vehiculo;
using System.Threading.Tasks;
using Revifast.Entities;

namespace revifast.test.UnitTesting
{
    class VehicleTests : GeneralInfo
    {


        [SetUp]
        public void InitVehicleInformation()
        {


        }


        [Test]
        public async Task RegisteranVehicle()
        {

            //Arrange


            var BDName = Guid.NewGuid().ToString();
            var context = BuildContext(BDName);


            //Act

            var newvehicle = new CreateVehiculoViewModel()
            {
                Placa = "ABC123",
                Color = "Azul",
                FechaFabricacion = "14/2020",
                ConductorId = 1,
                CategoriaId = 1,
                ModeloId = 1,
            };

            var controller = new VehiculoesController(context);

            var response = await controller.Create(newvehicle);
            var registration = response.Result as StatusCodeResult;

            //Assert

            Assert.IsInstanceOf<OkResult>(registration);
        }


        [Test]
        public async Task VehicleDetails()
        {

            //Arrange

            var BDName = Guid.NewGuid().ToString();
            var context = BuildContext(BDName);

            var fakevehicle = new Vehiculo()
            {
                Placa = "ABC123",
                Color = "Azul",
                FechaFabricacion = "14/2020",
                ConductorId = 1,
                CategoriaId = 1,
                ModeloId = 1,
            };

            var fakevehicle2 = new Vehiculo()
            {
                Placa = "XDX589",
                Color = "Rojo",
                FechaFabricacion = "21/2018",
                ConductorId = 1,
                CategoriaId = 1,
                ModeloId = 1,
            };



            context.Vehiculos.Add(fakevehicle);
            context.Vehiculos.Add(fakevehicle2);
            await context.SaveChangesAsync();

            var context2 = BuildContext(BDName);

            //Act
            var controller = new VehiculoesController(context2);

            var response = await controller.List();


            //Assert
            Assert.IsNotNull(response);
        }


        [Test]
        public async Task UpdateVehicle()
        {

            //Arrange

            var BDName = Guid.NewGuid().ToString();
            var context = BuildContext(BDName);

            var fakevehicle = new Vehiculo()
            {
                Placa = "ABC123",
                Color = "Azul",
                FechaFabricacion = "14/2020",
                ConductorId = 1,
                CategoriaId = 1,
                ModeloId = 1,
            };

            context.Vehiculos.Add(fakevehicle);
            await context.SaveChangesAsync();

            var context2 = BuildContext(BDName);

            //Act
            var controller = new VehiculoesController(context2);

            var fakevehicleUPT = new UpdateVehiculoViewModel()
            {
                VehiculoId = 1,
                Placa = "XYZ522",
                Color = "Rojo",
                FechaFabricacion = "14/2020",
                ConductorId = 1,
                CategoriaId = 1,
                ModeloId = 1,
            };
            
            var response = await controller.Update(fakevehicleUPT);
            var update = response as StatusCodeResult;

            //Assert
            Assert.AreEqual(200, update.StatusCode);

            var context3 = BuildContext(BDName);
            var exist = await context3.Vehiculos.FindAsync(1);
            Assert.IsNotNull(exist);

            
        }

        [Test]
        public async Task DeleteVehicle()
        {

            //Arrange

            var BDName = Guid.NewGuid().ToString();
            var context = BuildContext(BDName);

            var fakevehicle = new Vehiculo()
            {
                Placa = "ABC123",
                Color = "Azul",
                FechaFabricacion = "14/2020",
                ConductorId = 1,
                CategoriaId = 1,
                ModeloId = 1,
            };

            context.Vehiculos.Add(fakevehicle);
            await context.SaveChangesAsync();

            var context2 = BuildContext(BDName);

            //Act
            var controller = new VehiculoesController(context2);

            var response = await controller.Delete(1);
            var cancel = response as StatusCodeResult;

            //Assert
            Assert.AreEqual(200, cancel.StatusCode);

            var context3 = BuildContext(BDName);
            var exist = await context3.Vehiculos.FindAsync(1);
            Assert.IsNull(exist);
        }

    }
}
