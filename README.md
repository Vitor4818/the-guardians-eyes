﻿# 🌍 Sistema Inteligente de Monitoramento e Classificação de Riscos Ambientais

Este projeto tem como objetivo principal oferecer uma solução tecnológica e acessível para detectar, classificar e responder rapidamente a desastres naturais em áreas urbanas, por meio da integração de **IoT**, **visão computacional**, **inteligência artificial** e **sistemas web interativos**.

---

## 🧠 Contexto do Projeto

Desastres naturais, como deslizamentos de terra e enchentes, continuam a impactar gravemente vidas e estruturas urbanas. Muitas prefeituras carecem de ferramentas tecnológicas para monitorar, prever e responder de forma eficiente a esses eventos.

Pensando nisso, propusemos uma solução que une sensores ambientais, drones com visão computacional e IA, tudo integrado a uma API desenvolvida em .NET para monitoramento em tempo real, análise de gravidade e priorização de resposta.

---

## ✅ Solução Proposta

### 🛰️ Monitoramento com IoT
- Sensores posicionados em áreas de risco coletam dados como:
  - Velocidade do vento
  - Umidade do solo
  - Temperatura
  - Outros parâmetros ambientais relevantes

### 🚁 Apoio ao Resgate com Visão Computacional
- Drones sobrevoam automaticamente as áreas atingidas por desastres;
- Utilizam visão computacional para identificar **a presença de pessoas em risco**;
- Ao detectar uma pessoa:
  - A imagem e a **localização geográfica** são enviadas para a **API .NET**;
  - A imagem é repassada para a **API Python**, que classifica a **gravidade do local**;
  - A gravidade e a localização são então encaminhadas às **equipes de resgate**, permitindo que as áreas mais críticas tenham **prioridade no atendimento**.

> ⚠️ Importante: o sistema **não classifica o estado clínico da pessoa**, apenas a **gravidade da situação ambiental** ao redor.

### ⚙️ Processamento Inteligente com IA
- Imagens capturadas pelos drones e sensores são enviadas à **API Python**, que:
  - Classifica a **gravidade do desastre** (`leve`, `moderado` ou `pesado`);
  - Envia o resultado para a **API .NET**, que registra a ocorrência e dispara os alertas;
  - Permite priorização eficiente e baseada em dados reais.

### 📊 Visualização em Tempo Real
- **Dashboard interativa** com:
  - Alertas e ocorrências em tempo real;
  - Mapa georreferenciado;
  - Histórico completo de desastres;
  - Dados filtráveis e relatórios automáticos.

---

## 🧱 Estrutura do Projeto

### 🌐 API .NET (C#)
- Responsável pelo gerenciamento e registro das entidades:
  - `Usuario`
  - `Desastre`
  - `Local`
  - `Impacto`
  - `ImagensCapturadas`
  - `GrupoDesastre`
  - `SubgrupoDesastre`
  - `Drone`
  - `Sensores`
  - `TerrenoGeografico`
- Integra com a API Python para análise da gravidade;
- Encaminha dados para as equipes de resgate.

### 🧠 API Python (Classificação com IA)
A API Python foi desenvolvida com **Flask**, **TensorFlow** e **Keras** para classificar o nível de gravidade de um desastre natural a partir de uma imagem enviada via URL.

#### 📌 Funcionalidade:
- Recebe uma URL de imagem via requisição POST;
- Pré-processa a imagem;
- Utiliza modelo baseado na arquitetura **MobileNetV2**;
- Classifica como: `leve`, `moderado` ou `pesado`;
- Retorna a classe e o nível de confiança;
- Integra com a API .NET.

#### 🧪 Tecnologias utilizadas:
- **Flask** — criação da API REST
- **TensorFlow + Keras** — modelo de classificação
- **MobileNetV2** — arquitetura da rede neural
- **NumPy**, **Pillow**, **Requests**

---

## 📁 Estrutura do Projeto

| Projeto | Descrição |
|--------|-----------|
| **TheGuardiansEyesApi** | API Web principal com os **endpoints REST** (Controllers). Gerencia as rotas, autenticação, comunicação com as camadas de negócio e integração com APIs externas. |
| **TheGuardiansEyesBusiness** | Camada de **lógica de negócio**. Responsável pelas regras, validações e processamentos entre o front-end e os dados persistidos. |
| **TheGuardiansEyesData** | Camada de **acesso a dados**, implementada com **Entity Framework Core** conectando-se a um banco de dados **Oracle**. Realiza operações de CRUD. |
| **TheGuardiansEyesModel** | Projeto contendo as **entidades** que representam os modelos do sistema
| **TheGuardiansEyesMVC** | Projeto **MVC Web** para uso administrativo e visualização em tempo real. Interface web baseada em ASP.NET MVC. |

## Diagrama de Arquitetura do Sistema

O diagrama abaixo apresenta a arquitetura geral do **Sistema Inteligente de Monitoramento e Classificação de Riscos Ambientais**, evidenciando a interação entre os principais componentes que possibilitam a detecção e resposta eficiente a desastres naturais.

## Camadas da Arquitetura

### 1. Camada de Coleta de Dados
- **Sensores IoT** instalados em áreas de risco coletam dados ambientais.
- **Drones equipados com câmeras** realizam sobrevoos automáticos para captar imagens das regiões afetadas.

### 2. Camada de Processamento e Análise
- As imagens capturadas pelos drones são enviadas para a **API Python**, que utiliza inteligência artificial para classificar o nível de gravidade do desastre.
- A **API .NET** gerencia o registro das ocorrências, integra dados dos sensores e análises da IA, e encaminha informações relevantes para as equipes de resgate.

### 3. Camada de Visualização e Apoio à Resposta
- A aplicação manda dados da localização e gravidade para o front, no final teremos:
  - As localizações das pessoas detectadas em situação de risco.
  - A gravidade do perigo naquela área.
- Permite que as equipes de resgate priorizem suas ações com base em dados precisos e atualizados em tempo real.


<img src="https://github.com/user-attachments/assets/068f6639-e991-4762-820a-955e9b5f4eb8" width="400"/>


## ▶️ Como Executar o Projeto

#### 1. Clonar o repositório
```bash
git clone https://github.com/Vitor4818/the-guardians-eyes.git
```
#### 2. Configurar o Banco de Dados Oracle
No arquivo appsettings.json da API, configure a DefaultConnection com suas credenciais e fonte de dados:
```
    "DefaultConnection": "User Id=seu_usuario;Password=sua_senha;Data Source=seu_servidor;"
```
#### 3. Aplicar as Migrations
Gera o banco de dados a partir das migrations:
```
dotnet ef database update --project TheGuardiansEyesData --startup-project TheGuardiansEyesApi
```
#### 4. Executar a API .NET
```
cd TheGuardiansEyesApi
dotnet run webapi
```
#### 5. Executar projeto MVC
Abra um novo terminal e execute:
```
cd TheGuardiansEyesMVC
dotnet run
```

## Executar com Docker
1. Clonar o repositório:
```bash
git clone https://github.com/Vitor4818/the-guardians-eyes.git
cd the-guardians-eyes
```

2. Criar a rede docker:
```
docker network create fiap-gs
```
   
4. Subir o container do Oracle:
```
docker run -d --name oracle-database --network fiap-gs -p 1521:1521 -p 8080:8080 -e ORACLE_PASSWORD='F7uLw9kZ!mXv' -e ORACLE_DATABASE=ORCL -e APP_USER=appuser_n73X -e APP_USER_PASSWORD='F7uLw9kZ!mXv' -v oracle-database:/opt/oracle/oradata gvenzl/oracle-xe
```

4. Realizar o build da imagem da aplicação
```
docker build -t theguardianseyes:1.0 .   
```

6. Rodar o container da aplicação:
```bash
docker run -d --name theguardianseyes-api --network fiap-gs -e "ConnectionStrings__DefaultConnection=User Id=appuser_n73X;Password=F7uLw9kZ!mXv;Data Source=oracle-database:1521/XEPDB1;" -p 5193:5193 theguardianseyes:1.0
```
A API será iniciada

## Testes
Este repositório contém arquivos de teste .http para a API da aplicação TheGuardiansEyesModel, que implementa o CRUD para os principais modelos do sistema.

### Pré-requisitos
- Ter o Visual Studio Code instalado: https://code.visualstudio.com/

- Ter a extensão REST Client instalada no VS Code: REST Client - Visual Studio Marketplace

### Como usar os arquivos de teste .http
Supondo que o projeto já esteja baixado e aberto no Visual Studio Code, siga os passos abaixo para executar os testes:

1. Acesse a pasta `TestesHttp` dentro do diretório do projeto.
2. Abra o arquivo `.http` correspondente ao recurso que deseja testar.
3. Cada requisição dentro do arquivo estará separada, e acima de cada uma aparecerá o botão **"Send Request"**.
4. Clique no botão **"Send Request"** para enviar a requisição à API.
5. A resposta será exibida na janela lateral do Visual Studio Code.

Repita esse procedimento para todas as requisições que desejar testar.

### Exemplo de JSON para requisição de criação ou atualização de Drone

Abaixo está um exemplo de corpo JSON que pode ser utilizado para criar ou atualizar um registro de drone na API. Certifique-se de enviar os dados no formato correto para evitar erros de validação.

```json
{
  "id": 1,
  "fabricante": "DJI",
  "modelo": "Phantom 4",
  "tempoVoo": "30 minutos",
  "distanciaMaxima": 5000,
  "velocidadeMaxima": 72,
  "camera": "4K",
  "peso": 1380
}

```

## 🛠️ Tecnologias Utilizadas

### Backend
- **.NET / .NET Core** — desenvolvimento da API REST (`TheGuardiansEyesApi`), aplicação MVC (`TheGuardiansEyesMVC`) e classlibs (`TheGuardiansEyesBusiness`, `TheGuardiansEyesData`, `TheGuardiansEyesModel`).
- **C#** — linguagem principal do backend.

### Banco de Dados
- **Oracle Database** — sistema gerenciador de banco de dados relacional para armazenamento persistente.
- **Entity Framework Core** — ORM para mapeamento objeto-relacional e acesso a dados no Oracle.

### Inteligência Artificial e Visão Computacional
- **Python** — desenvolvimento da API de classificação da gravidade ambiental.
- **Flask** — framework para criação da API REST em Python.
- **TensorFlow e Keras** — desenvolvimento e execução do modelo de classificação (MobileNetV2).
- **NumPy, Pillow, Requests** — bibliotecas para manipulação de imagens e requisições HTTP.

### Frontend
- **ASP.NET MVC** — interface web administrativa minimalista.

### Outras Ferramentas
- **Git** — controle de versão.
- **Visual Studio / Visual Studio Code** — IDEs para desenvolvimento.
- **Docker** — execução em containers
