using Microsoft.EntityFrameworkCore;
using ReHope.Applications.Regras;
using ReHope.Domains;
using ReHope.DTOs.UsuarioDto;
using ReHope.Exceptions;
using ReHope.Interfaces;

namespace ReHope.Applications.Services
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioService(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        public List<LerUsuarioDto> Listar()
        {
            List<Usuario> usuarios = _repository.Listar();

            List<LerUsuarioDto> usuariosDto = usuarios.Select(usuario => new LerUsuarioDto
            {
                UsuarioID = usuario.UsuarioID,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Telefone = usuario.Telefone
            }).ToList();

            return usuariosDto;
        }

        public LerUsuarioDto BuscarPorId(Guid usuarioId)
        {
            Usuario usuario = _repository.ObterPorId(usuarioId);

            if(usuario == null)
            {
                throw new DomainException("Usuário não encontrado.");
            }

            LerUsuarioDto usuarioDto = new LerUsuarioDto
            {
                UsuarioID = usuario.UsuarioID,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Telefone = usuario.Telefone
            };

            return usuarioDto;
        }

        public LerUsuarioDto BuscarPorEmail(string email)
        {
            Usuario usuario = _repository.ObterPorEmail(email);

            if (usuario == null)
            {
                throw new DomainException("Usuário não encontrado.");
            }

            LerUsuarioDto usuarioDto = new LerUsuarioDto
            {
                UsuarioID = usuario.UsuarioID,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Telefone = usuario.Telefone
            };

            return usuarioDto;
        }

        public void Adicionar (CriarUsuarioDto usuarioDto)
        {
            Validacao.ValidarNome(usuarioDto.Nome);
            Validacao.ValidarEmail(usuarioDto.Email);

            if(_repository.EmailExiste(usuarioDto.Email))
            {
                throw new DomainException("Já existe um usuário com esse e-mail.");
            }

            //Usuario usuario = new Usuario
            //{
            //    Nome = usuarioDto.Nome,
            //    Email = usuarioDto.Email,
            //    Senha = HashSenha(usuarioDto.Senha),
            //    Telefone = usuarioDto.Telefone
            //}
        }
    }
}
