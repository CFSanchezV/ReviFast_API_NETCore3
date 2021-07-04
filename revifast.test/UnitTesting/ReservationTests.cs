using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Revifast.Api.Controllers;
using Revifast.Api.Models.Reserva;
using System.Threading.Tasks;
using Revifast.Entities;

namespace revifast.test.UnitTesting
{
    class ReservationTests : GeneralInfo
    {


        [SetUp]
        public void InitReservation()
        {


        }


        [Test]
        public async Task MakeaReservation()
        {

            //Arrange


            var BDName = Guid.NewGuid().ToString();
            var context = BuildContext(BDName);


            //Act

            var newreservation = new CreateReservaViewModel()
            {
                Fecha = "12/03/20",
                Hora = "12:00",
                Observaciones = "Color azul",
                VehiculoId = 1,
                LocalId = 1,
                ReservaEstadoId = 1,
            };

            var controller = new ReservasController(context);

            var response = await controller.Create(newreservation);
            var reservation = response.Result as StatusCodeResult;

            //Assert

            Assert.IsInstanceOf<OkResult>(reservation);
        }


        [Test]
        public async Task ListReservations()
        {

            //Arrange

            var BDName = Guid.NewGuid().ToString();
            var context = BuildContext(BDName);

            var fakereservation = new Reserva()
            {
                Fecha = "12/03/20",
                Hora = "12:00",
                Observaciones = "Color azul",
                VehiculoId = 1,
                LocalId = 1
            };

            var fakereservation2 = new Reserva()
            {
                Fecha = "12/03/21",
                Hora = "15:00",
                Observaciones = "Color rosa",
                VehiculoId = 2,
                LocalId = 1
            };

            

            context.Reservas.Add(fakereservation);
            context.Reservas.Add(fakereservation2);
            await context.SaveChangesAsync();

            var context2 = BuildContext(BDName);

            //Act
            var controller = new ReservasController(context2);

            var response = await controller.List();


            //Assert
            Assert.IsNotNull(response);
        }


        [Test]
        public async Task CancelReservation()
        {

            //Arrange

            var BDName = Guid.NewGuid().ToString();
            var context = BuildContext(BDName);

            var fakereservation = new Reserva()
            {
                Fecha = "12/03/20",
                Hora = "12:00",
                Observaciones = "Color azul",
                VehiculoId = 1,
                LocalId = 1
            };

            context.Reservas.Add(fakereservation);
            await context.SaveChangesAsync();

            var context2 = BuildContext(BDName);

            //Act
            var controller = new ReservasController(context2);

            var response = await controller.Delete(1);
            var cancel = response as StatusCodeResult;

            //Assert
            Assert.AreEqual(200, cancel.StatusCode);

            var context3 = BuildContext(BDName);
            var exist = await context3.Reservas.FindAsync(1);
            Assert.IsNull(exist);
        }

    }
}
