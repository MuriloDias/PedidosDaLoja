Criação: Murilo Dias Firmino Felipin

Anotações sobre o projeto:

• Objetivo
	 Criar uma WebAPI em ASP.NET que permita gerenciar os pedidos de uma loja.
• Rotas: (a WebAPI deve conter rotas para)
	o Iniciar um novo pedido
	o Adicionar produtos ao pedido
	o Remover produtos do pedido
	o Fechar o pedido
	o Listar os pedidos
	o Obter um pedido (e seu produtos) através do ID
• Condições:
	o Produtos não podem ser adicionados/removidos em pedidos fechados
	o Um pedido só pode ser fechado caso contenha ao menos um produto

Sistema Utiliza Seguintes Tecnologias:
	.NET SDK:
	 Version:           8.0.403
	 Commit:            c64aa40a71
	 Workload version:  8.0.400-manifests.e99c892e
	 MSBuild version:   17.11.9+a69bbaaf5

OBS: - Não consegui acertar nos testes pelo tempo, mas acredito que pesquisando um pouco mais acertaria para poder dar continuidade
de fato não seriam testes dificeis, usando mock para forjar o retorno do DB e testando as possibilidades de todas as regras de negocio possiveis,
acertando um, deveria ficar bem fácil de fazer todos, por que não são dificeis de se fazer, mas por tempo, eu deixei a estrutura criada e vou enviar desta forma.
 - Não foi utilizado a estrutura de pastas completa da arquitetura indicada, pois várias pastas ficaram vazias, então foram removidas, e seriam recriadas conforme a necessidade.