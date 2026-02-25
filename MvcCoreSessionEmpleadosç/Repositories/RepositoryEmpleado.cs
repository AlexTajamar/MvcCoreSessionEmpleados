using Microsoft.EntityFrameworkCore;
using MvcCoreSessionEmpleados.Data;
using MvcCoreSessionEmpleados.Models;

namespace MvcCoreSessionEmpleados.Repositories
{

    public class RepositoryEmpleado
    {
        private  HospitalContext context;

        public RepositoryEmpleado(HospitalContext context)
        {
            this.context = context;
        }

        public async Task<List<Empleado>> GetAllEmpleadosAsync()
        {
            var consulta = from datos in this.context.Empleados
                           select datos;
            return await consulta.ToListAsync();
        }

        public async Task<Empleado> FindEmpleadoAsync(int id)
        {
            var consulta = from datos in this.context.Empleados
                           where datos.emp_no == id
                           select datos;
            return await consulta.FirstOrDefaultAsync();  
        }

        public async Task<List<Empleado>> GetEmpleadosByColeccionIdsAsync(List<int> ids)
        {
                        var consulta = from datos in this.context.Empleados
                           where ids.Contains(datos.emp_no)
                           select datos;
            return await consulta.ToListAsync();
        }
    }
}
