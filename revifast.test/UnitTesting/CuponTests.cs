using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Revifast.Api.Controllers;
using Revifast.Api.Models.Promocion;
using Revifast.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace revifast.test.UnitTesting
{
    class CuponTests : GeneralInfo
    {


        [SetUp]
        public void InitCupon()
        {


        }


        [Test]
        public async Task AddaCupon()
        {

            //Arrange


            var BDName = Guid.NewGuid().ToString();
            var context = BuildContext(BDName);


            //Act

            var newcupon = new CreatePromocionViewModel()
            {
                Nombre = "Revifast promo",
                Descuento = 1,
                Descripcion = "Registrarse en la app",
                ServicioId = 1,
            };

            var controller = new PromocionesController(context);

            var response = await controller.Create(newcupon);
            var registration = response.Result as StatusCodeResult;

            //Assert

            Assert.IsInstanceOf<OkResult>(registration);
        }


        [Test]
        public async Task CuponDetails()
        {

            //Arrange

            var BDName = Guid.NewGuid().ToString();
            var context = BuildContext(BDName);

            var fakecupon = new Promocion()
            {
                Nombre = "Revifast promo",
                Descuento = 1,
                Descripcion = "Registrarse en la app",
                ServicioId = 1,
            };

            var fakecupon2 = new Promocion()
            {
                Nombre = "Revifast promo 2",
                Descuento = 1,
                Descripcion = "Iniciar sesion en la app",
                ServicioId = 1,
            };



            context.Promociones.Add(fakecupon);
            context.Promociones.Add(fakecupon2);
            await context.SaveChangesAsync();

            var context2 = BuildContext(BDName);

            //Act
            var controller = new PromocionesController(context2);

            var response = await controller.List();


            //Assert
            Assert.IsNotNull(response);
        }


        [Test]
        public async Task UpdateCupon()
        {

            //Arrange

            var BDName = Guid.NewGuid().ToString();
            var context = BuildContext(BDName);

            var fakecupon = new Promocion()
            {
                Nombre = "Revifast promo",
                Descuento = 1,
                Descripcion = "Registrarse en la app",
                ServicioId = 1,
            };

            context.Promociones.Add(fakecupon);
            await context.SaveChangesAsync();

            var context2 = BuildContext(BDName);

            //Act
            var controller = new PromocionesController(context2);

            var fakecuponUPT = new UpdatePromocionViewModel()
            {
                Nombre = "Revifast promo 2",
                Descuento = 1,
                Descripcion = "Iniciar sesion en la app",
                ServicioId = 1,
            };

            var response = await controller.Update(fakecuponUPT);
            var update = response as StatusCodeResult;

            //Assert
            Assert.AreEqual(400, update.StatusCode);

            var context3 = BuildContext(BDName);
            var exist = await context3.Promociones.FindAsync(1);
            Assert.IsNotNull(exist);


        }

        [Test]
        public async Task DeleteCupon()
        {

            //Arrange

            var BDName = Guid.NewGuid().ToString();
            var context = BuildContext(BDName);

            var fakecupon = new Promocion()
            {
                Nombre = "Revifast promo",
                Descuento = 1,
                Descripcion = "Registrarse en la app",
                ServicioId = 1,
            };

            context.Promociones.Add(fakecupon);
            await context.SaveChangesAsync();

            var context2 = BuildContext(BDName);

            //Act
            var controller = new PromocionesController(context2);

            var response = await controller.Delete(1);
            var cancel = response as StatusCodeResult;

            //Assert
            Assert.AreEqual(200, cancel.StatusCode);

            var context3 = BuildContext(BDName);
            var exist = await context3.Promociones.FindAsync(1);
            Assert.IsNull(exist);
        }

    }
}