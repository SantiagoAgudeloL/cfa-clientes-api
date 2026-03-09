using CFA.Clientes.Api.Application.DTOs;
using CFA.Clientes.Api.Application.Ports;
using CFA.Clientes.Api.Domain.Entities;
using CFA.Clientes.Api.Infrastructure.Repositories;

namespace CFA.Clientes.Api.Application.UseCases
{
    public class TelefonoUseCase
    {
        private readonly ITelefonoRepository _repository;
        private readonly IClienteRepository _clienteRepository;

        public TelefonoUseCase(
      ITelefonoRepository repository,
      IClienteRepository clienteRepository)
        {
            _repository = repository;
            _clienteRepository = clienteRepository;
        }



        public async Task<int> CrearTelefono(int clienteId, TelefonoRequestDto dto)
        {
            var cliente = await _clienteRepository.ObtenerPorId(clienteId);

            if (cliente == null)
                throw new Exception("El cliente no existe");

            var telefono = new Telefono
            {
                ClienteCodigo = clienteId,
                TelefonoNumero = dto.Telefono
            };

            return await _repository.CrearTelefono(telefono);
        }

        public async Task<List<Telefono>> ObtenerTelefonosCliente(int clienteId)
        {
            return await _repository.ObtenerTelefonosCliente(clienteId);
        }

        public async Task EliminarTelefono(int telefonoId)
        {
            await _repository.EliminarTelefono(telefonoId);
        }
    }
}
