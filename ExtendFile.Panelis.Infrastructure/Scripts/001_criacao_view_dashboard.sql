--Não foi aplicado em produção
CREATE OR REPLACE VIEW vw_dashboard AS
SELECT
    -- Total de gatos ativos
    (SELECT COUNT(*)
     FROM "Cats" c
     WHERE c."IsActive" = true) AS "totalcats",

    -- Total de prédios (houses)
    (SELECT COUNT(*)
     FROM "Houses") AS "totalhouses",

    -- Total de boxes
    (SELECT COUNT(*)
     FROM "Boxes") AS "totalboxes",

    -- Total de boxes 100% ocupados (considerando apenas gatos ativos)
    (
        SELECT COUNT(*)
        FROM "Boxes" b
                 LEFT JOIN (
            SELECT c."BoxId", COUNT(*) AS "TotalCats"
            FROM "Cats" c
            WHERE c."BoxId" IS NOT NULL
              AND c."IsActive" = true
            GROUP BY c."BoxId"
        ) c ON c."BoxId" = b."Id"
        WHERE COALESCE(c."TotalCats", 0) >= b."MaxQuantity"
    ) AS "totalfullboxes";