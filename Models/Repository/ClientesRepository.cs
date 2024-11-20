using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace CadastroClientes.Models.Repository
{
    public class ClientesRepository
    {

        public void Salvar(Clientes clientes)
        {

            var listaClientes = Listar();
            var item = listaClientes.Where(t => t.Documento == clientes.Documento).FirstOrDefault();
         
            //se ele existe eu deleto
            if (item != null)
            {
                Deletar(clientes.Documento);
            }
         var clientesTexto = JsonConvert.SerializeObject(clientes) + "," + Environment.NewLine;
           
            File.AppendAllText("C:\\Users\\drago\\source\\repos\\CadastroClientes\\BancoDados\\BancoDados.txt", clientesTexto );

        }
        public List<Clientes> Listar()
        {

         var clientes = File.ReadAllText("C:\\Users\\drago\\source\\repos\\CadastroClientes\\BancoDados\\BancoDados.txt");

            List <Clientes> clientesLista = JsonConvert.DeserializeObject<List<Clientes>>("[" + clientes + "]");


            return clientesLista.OrderByDescending(t=>t.Nome).ToList();
        }
        public bool Deletar (string Documento)
        {
            //listei todos os itens
            var listaClientes = Listar();
            var item = listaClientes.Where(t => t.Documento == Documento).FirstOrDefault();
            if (item != null) 
                //remover itens da lista
            { 
                listaClientes.Remove(item);
                //limpar banco de dados
                File.WriteAllText("C:\\Users\\drago\\source\\repos\\CadastroClientes\\BancoDados\\BancoDados.txt", string.Empty);
                //escrever novamente o banco de dados a lista sem o item excluido
                foreach(var  cliente in listaClientes)
                {
                    Salvar(cliente);
                }
                return true;

            }
            return false;

        }
        public Clientes GetCliente(string Documento)
        {
            var clienteLista = Listar();
            var item = clienteLista.Where(t => t.Documento == Documento).FirstOrDefault();
            return item;    
        }
        
    }
}
