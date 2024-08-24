CREATE TABLE IF NOT EXISTS claims (
    id SERIAL PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    verified BOOLEAN NOT NULL
);

INSERT INTO claims (name, verified)
VALUES 
    ('Default Claim 1', false),
    ('Default Claim 2', false),
    ('Default Claim 3', false),
    ('Default Claim 4', false),
    ('Default Claim 5', false),
    ('Default Claim 6', false),
    ('Default Claim 7', false),
    ('Default Claim 8', false),
    ('Default Claim 9', false),
    ('Default Claim 10', false);