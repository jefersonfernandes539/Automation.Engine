# 🎵 Template Backend NestJS

Um **projeto backend** construído com **NestJS**, **Prisma ORM** e documentação com **Swagger (OpenAPI)**.  
Esse template foi pensado para aplicações **multimódulo** e escaláveis, servindo como base para projetos reais.

---

## 🚀 Tecnologias

- **[NestJS](https://nestjs.com/)** — framework Node.js progressivo
- **[Prisma ORM](https://www.prisma.io/)** — ORM moderno
- **[PostgreSQL](https://www.postgresql.org/)** — banco de dados relacional
- **[Swagger](https://swagger.io/)** — documentação interativa da API
- **TypeScript**

---

## 📂 Estrutura do Projeto

```bash
.
├── prisma/
│   ├── migrations/          # Migrations do banco
│   ├── schema.prisma        # Modelos Prisma
│   └── seed.ts              # Script para popular o banco
├── src/
│   ├── app/                 # Módulo principal
│   ├── auth/                # Autenticação (JWT, etc.)
│   ├── booking/             # Reservas
│   ├── chat/                # Chat em tempo real
│   ├── client-profile/      # Perfis de clientes
│   ├── database/            # Configuração do banco
│   ├── musician/            # Módulo de músicos
│   ├── rating/              # Avaliações
│   ├── schedule/            # Agenda
│   ├── user/                # Usuários
│   ├── venue/               # Locais
│   ├── main.ts              # Ponto de entrada da aplicação
├── .env                     # Variáveis de ambiente
├── .gitignore
└── README.md
⚙️ Configuração
1. Clone o repositório
bash
Copiar código
git clone https://github.com/seu-usuario/template-backend-nestjs.git
cd template-backend-nestjs
2. Instale as dependências
bash
Copiar código
npm install
3. Configure o .env
env
Copiar código
DATABASE_URL="postgresql://user:password@localhost:5432/dbname?schema=public"
PORT=3000
JWT_SECRET=super_secret_key
🗂️ Prisma ORM
Gerar o cliente Prisma:

bash
Copiar código
npx prisma generate
Rodar migrations:

bash
Copiar código
npx prisma migrate dev --name init
Popular banco com seed:

bash
Copiar código
npx prisma db seed
🏃 Rodando o projeto
Desenvolvimento
bash
Copiar código
npm run start:dev
Produção
bash
Copiar código
npm run build
npm run start:prod
📚 Swagger
Após rodar o projeto, acesse:

👉 http://localhost:3000/api

✅ Scripts úteis
bash
Copiar código
npm run start       # inicia aplicação
npm run start:dev   # inicia em modo dev
npm run build       # build para produção
npm run lint        # checa lint
npm run test        # executa testes
📦 Melhorias futuras
Multi-tenancy (multi-tenant databases)

Autenticação OAuth2 / Social Login

Cache com Redis

Monitoramento com OpenTelemetry

Integração com filas (BullMQ)

📝 Licença
MIT — fique à vontade para usar e modificar 🚀

yaml
Copiar código

---

👉 Quer que eu já monte esse `README.md` em **formato de arquivo** pra você baixar direto?
