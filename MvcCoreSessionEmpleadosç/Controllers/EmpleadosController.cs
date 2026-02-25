using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MvcCoreSessionEmpleados.Extensions;
using MvcCoreSessionEmpleados.Models;
using MvcCoreSessionEmpleados.Repositories;

namespace MvcCoreSessionEmpleados.Controllers
{
    public class EmpleadosController : Controller
    {
        private RepositoryEmpleado repo;
        public EmpleadosController(RepositoryEmpleado repo)
        {
            this.repo = repo;
        }
        public async Task<IActionResult> Index(int? salario)
        {
            if (salario != null)
            {
                int sumaTotal = 0;

                if (HttpContext.Session.GetString("SUMASALARIAL") != null)
                {
                    sumaTotal = HttpContext.Session.getObject<int>("SUMASALARIAL");
                }

                sumaTotal += salario.Value;

                HttpContext.Session.setObject("SUMASALARIAL", sumaTotal);

                ViewData["MENSAJE"] = "SALARIO ALMACENADO";
            }

            List<Empleado> empleados = await this.repo.GetAllEmpleadosAsync();
            return View(empleados);
        }

        public IActionResult SumaSalarial()
        {
            return View();
        }


        public IActionResult IndexSessionSalarios()
        {
            return View();
        }

        public async Task<IActionResult> SessionEmpleado(int? idempleado)
        {
            if (idempleado != null)
            {
                Empleado empleado =
                    await this.repo.FindEmpleadoAsync(idempleado.Value);
                //EN SESSION TENDREMOS ALMACENADOS UN CONJUNTO DE EMPLEADOS
                List<Empleado> empleadosList;

                //DEBEMOS PREGUNTAR SI YA TENEMOS EMPLEADOS EN SESSION
                if (HttpContext.Session.getObject<List<Empleado>>("EMPLEADOS") != null)
                {
                    //RECUPERAMOS LA LISTA DE SESSION
                    empleadosList = HttpContext.Session.getObject<List<Empleado>>("EMPLEADOS");
                }
                else
                {
                    //CREAMOS UNA NUEVA LISTA PARA ALMACENAR LOS EMPLEADOS
                    empleadosList = new List<Empleado>();
                }
                //AGREGAMOS EL EMPLEADO AL LIST
                empleadosList.Add(empleado);
                //ALMACENAMOS LA LISTA EN SESSION
                HttpContext.Session.setObject("EMPLEADOS", empleadosList);
                ViewData["MENSAJE"] = "Empleado " + empleado.apellido + " almacenado correctamente";
            }
            List<Empleado> empleados = await this.repo.GetAllEmpleadosAsync();
            return View(empleados);
        }

        public IActionResult EmpleadosAlmacenados()
        {
            return View();
        }

        public async Task<IActionResult> SessionEmpleadosOk(int? idempleado)
        {
            if (idempleado != null)
            {
                //ALMACENAMOS LO MINIMO
                List<int> idsEmpleados;
                if (HttpContext.Session.getObject<List<int>>("ids") != null)
                {
                    //RECUPERAMOS LA COLEECION
                    idsEmpleados = HttpContext.Session.getObject<List<int>>("ids");
                }
                else
                {
                    idsEmpleados = new List<int>();
                }
                //ALMACENAMOS EL ID
                idsEmpleados.Add(idempleado.Value);
                //ALMACENAMOS EN SESSION LOS DATOS
                HttpContext.Session.setObject("ids", idsEmpleados);
                ViewData["MENSAJE"] = "Empleados OK almacenados";



            }
            List<Empleado> emppleados = await this.repo.GetAllEmpleadosAsync();
            return View(emppleados);
        }

        public async Task<IActionResult> EmpleadosAlmacenadosOK()
        {
            List<int> ids = HttpContext.Session.getObject<List<int>>("ids");
            if (ids == null)
            {
                ViewData["MENSAJE"] = "no hay nada en la lista";
                return View();
            }
            else
            {
                List<Empleado> emps = await this.repo.GetEmpleadosByColeccionIdsAsync(ids);
                return View(emps);
            }
        }


        public async Task<IActionResult> SessionEmpleadosV4(int? idempleado)

        {

            //RECUPERAMOS LA COLECCION

            List<int> idsEmpleados = HttpContext.Session.getObject<List<int>>("ids");

            if (idsEmpleados == null)

            {

                //creamos la coleccion

                idsEmpleados = new List<int>();

            }

            if (idempleado != null)

            {

                //Agregamos el id del empleado

                idsEmpleados.Add(idempleado.Value);

                HttpContext.Session.setObject("ids", idsEmpleados);

                ViewData["MENSAJE"] = "Empleado almacenado: " + idempleado.Value;

            }

            List<Empleado> empleados = await this.repo.GetEmpleadosAsyncNotIn(idsEmpleados);

            return View(empleados);

        }

        public async Task<IActionResult> EmpleadosAlmacenadosV4()

        {

            List<int> idsEmpleados = HttpContext.Session.getObject<List<int>>("ids");

            if (idsEmpleados == null)

            {

                ViewData["MENSAJE"] = "No hay empleados almacenados en session";

                return View();

            }

            else

            {

                List<Empleado> empleados = await this.repo.GetEmpleadosByColeccionIdsAsync(idsEmpleados);

                return View(empleados);

            }

        }

    }
}
