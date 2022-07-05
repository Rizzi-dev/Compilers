namespace VerySimpleInterpreter
{
    public class SymbolTable
    {

        private Dictionary<string,double?> _data;

        public SymbolTable()
        {
            _data = new Dictionary<string, double?>();
        }

        public void Put(string name, double? value = null){
            _data.Add(name, value);
        }
        
        public double? Get(string name){
            return _data[name];
        }
    
    }
}