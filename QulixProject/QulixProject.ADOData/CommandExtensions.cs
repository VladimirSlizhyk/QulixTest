using System;
using System.Data;

namespace QulixProject.ADOData
{
    public static class CommandExtensions
    {
        public static void AddParameter(this IDbCommand command, string name, object value) //метод для добавления параметров в параметризированные запросы
        {
            if (command == null) throw new ArgumentNullException("command");
            if (name == null) throw new ArgumentNullException("name");

            var p = command.CreateParameter();
            p.ParameterName = name;
            p.Value = value ?? DBNull.Value;
            command.Parameters.Add(p);
        }
    }
}
