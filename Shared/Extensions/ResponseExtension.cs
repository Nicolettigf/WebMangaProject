using FluentValidation.Results;
using Shared.Responses;
using System.Text;

namespace Shared.Extensions
{
    //MÉTODOS DE EXTENSÃO - UTILIDADE: Diminuir a quantidade de objetos que o programador precisa gravar,
    //criando a ilusão que determinados objetos possuem métodos. (que na realidade não são deles)

    //Regras: 1)O método deve estar em uma classe static
    //        2)O método deve ser static
    //        3)O primeiro parâmetro do método deve conter a palavra "this" seguida do objeto que queremos estender

    public static class ResponseExtension
    {
        public static Response ConvertToResponse(this ValidationResult result)
        {
            Response response = ResponseFactory.CreateInstance().CreateSuccessResponse();
            if (result.IsValid)
            {
                response.HasSuccess = true;
                response.Message = "Operação efetuada com sucesso.";
                return response;
            }

            StringBuilder builder = new StringBuilder();
            foreach (ValidationFailure fail in result.Errors)
            {
                builder.AppendLine(fail.ErrorMessage);
            }

            response.HasSuccess = false;
            response.Message = builder.ToString();
            return response;
        }
    }

    
}
