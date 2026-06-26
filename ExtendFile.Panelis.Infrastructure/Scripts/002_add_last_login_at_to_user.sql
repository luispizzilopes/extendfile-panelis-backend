-- Adiciona coluna LastLoginAt na tabela de usuários (AspNetUsers)
-- Operação segura: coluna nullable, sem impacto em registros existentes
ALTER TABLE "AspNetUsers"
    ADD COLUMN IF NOT EXISTS "LastLoginAt" timestamp with time zone NULL;

-- Rollback:
-- ALTER TABLE "AspNetUsers" DROP COLUMN IF EXISTS "LastLoginAt";
