using NUnit.Framework;
using Revifast.Api.Controllers;
using Revifast.Api.Models.Horario;
using Revifast.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace revifast.test.UnitTesting
{
    class ScheduleTests : GeneralInfo
    {


        [SetUp]
        public void InitSchedule()
        {


        }


        [Test]
        public async Task AddaSchedule()
        {

            //Arrange


            var BDName = Guid.NewGuid().ToString();
            var context = BuildContext(BDName);


            //Act

            var newschedule = new CreateHorarioViewModel()
            {
                HoraApertura = "8:00",
                HoraCierre = "21:00",
                Dia = "Viernes",
                Feriado = false,
            };

            var controller = new HorariosController(context);

            var response = await controller.Create(newschedule);

            //Assert

            Assert.IsNotNull(response);
        }


        [Test]
        public async Task ListCompany()
        {

            //Arrange

            var BDName = Guid.NewGuid().ToString();
            var context = BuildContext(BDName);

            var fakeschedule = new Horario()
            {
                HoraApertura = "8:00",
                HoraCierre = "21:00",
                Dia = "Viernes",
                Feriado = false,
            };

            var fakeschedule2 = new Horario()
            {
                HoraApertura = "8:00",
                HoraCierre = "18:00",
                Dia = "Sábado",
                Feriado = false,
            };



            context.Horarios.Add(fakeschedule);
            context.Horarios.Add(fakeschedule2);
            await context.SaveChangesAsync();

            var context2 = BuildContext(BDName);

            //Act
            var controller = new HorariosController(context2);

            var response = await controller.List();


            //Assert
            Assert.IsNotNull(response);
        }


    }
}

