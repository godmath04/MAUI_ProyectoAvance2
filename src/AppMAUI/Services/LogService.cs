using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MAUI_ProyectoAvance2.Services
{
    public class LogService
    {
        private readonly string logPath;

        public LogService()
        {
            logPath = Path.Combine(FileSystem.AppDataDirectory, "log.txt");
        }

        public void EscribirLog(string mensaje)
        {
            string entrada = $"{DateTime.Now}: {mensaje}\n";
            File.AppendAllText(logPath, entrada);
        }

        public string LeerLog()
        {
            if (File.Exists(logPath))
                return File.ReadAllText(logPath);
            return "No hay registros.";
        }

        public void BorrarLog()
        {
            if (File.Exists(logPath))
                File.Delete(logPath);
        }
    }
}
