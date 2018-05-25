--
-- PostgreSQL database cluster dump
--

-- Started on 2018-05-25 15:00:00 UTC

SET default_transaction_read_only = off;

SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;

--
-- Roles
--

CREATE ROLE postgres;
ALTER ROLE postgres WITH SUPERUSER INHERIT CREATEROLE CREATEDB LOGIN REPLICATION BYPASSRLS;
CREATE ROLE projectsvc;
ALTER ROLE projectsvc WITH SUPERUSER INHERIT NOCREATEROLE NOCREATEDB LOGIN NOREPLICATION NOBYPASSRLS PASSWORD 'md5a764a247f03eb14e18b265f8a63b1f39';






--
-- Database creation
--

CREATE DATABASE project WITH TEMPLATE = template0 OWNER = postgres;
REVOKE CONNECT,TEMPORARY ON DATABASE template1 FROM PUBLIC;
GRANT CONNECT ON DATABASE template1 TO PUBLIC;


\connect postgres

SET default_transaction_read_only = off;

--
-- PostgreSQL database dump
--

-- Dumped from database version 9.6.9
-- Dumped by pg_dump version 10.3

-- Started on 2018-05-25 15:00:00 UTC

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 2119 (class 0 OID 0)
-- Dependencies: 2118
-- Name: DATABASE postgres; Type: COMMENT; Schema: -; Owner: postgres
--

COMMENT ON DATABASE postgres IS 'default administrative connection database';


--
-- TOC entry 1 (class 3079 OID 12390)
-- Name: plpgsql; Type: EXTENSION; Schema: -; Owner: 
--

CREATE EXTENSION IF NOT EXISTS plpgsql WITH SCHEMA pg_catalog;


--
-- TOC entry 2121 (class 0 OID 0)
-- Dependencies: 1
-- Name: EXTENSION plpgsql; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION plpgsql IS 'PL/pgSQL procedural language';


-- Completed on 2018-05-25 15:00:00 UTC

--
-- PostgreSQL database dump complete
--

\connect project

SET default_transaction_read_only = off;

--
-- PostgreSQL database dump
--

-- Dumped from database version 9.6.9
-- Dumped by pg_dump version 10.3

-- Started on 2018-05-25 15:00:00 UTC

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 1 (class 3079 OID 12390)
-- Name: plpgsql; Type: EXTENSION; Schema: -; Owner: 
--

CREATE EXTENSION IF NOT EXISTS plpgsql WITH SCHEMA pg_catalog;


--
-- TOC entry 2179 (class 0 OID 0)
-- Dependencies: 1
-- Name: EXTENSION plpgsql; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION plpgsql IS 'PL/pgSQL procedural language';


--
-- TOC entry 2 (class 3079 OID 16386)
-- Name: uuid-ossp; Type: EXTENSION; Schema: -; Owner: 
--

CREATE EXTENSION IF NOT EXISTS "uuid-ossp" WITH SCHEMA public;


--
-- TOC entry 2180 (class 0 OID 0)
-- Dependencies: 2
-- Name: EXTENSION "uuid-ossp"; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION "uuid-ossp" IS 'generate universally unique identifiers (UUIDs)';


SET default_tablespace = '';

SET default_with_oids = false;

--
-- TOC entry 190 (class 1259 OID 16434)
-- Name: members; Type: TABLE; Schema: public; Owner: projectsvc
--

CREATE TABLE public.members (
    project_id uuid NOT NULL,
    user_id uuid NOT NULL,
    role_id uuid,
    last_access timestamp with time zone
);


ALTER TABLE public.members OWNER TO projectsvc;

--
-- TOC entry 186 (class 1259 OID 16397)
-- Name: permission_defaults; Type: TABLE; Schema: public; Owner: projectsvc
--

CREATE TABLE public.permission_defaults (
    id uuid DEFAULT public.uuid_generate_v4() NOT NULL,
    name text,
    min_value integer DEFAULT 0,
    max_value integer DEFAULT 1,
    default_value integer DEFAULT 0
);


ALTER TABLE public.permission_defaults OWNER TO projectsvc;

--
-- TOC entry 187 (class 1259 OID 16411)
-- Name: permissions; Type: TABLE; Schema: public; Owner: projectsvc
--

CREATE TABLE public.permissions (
    id uuid DEFAULT public.uuid_generate_v4() NOT NULL,
    permission_default_id uuid,
    role_id uuid,
    value integer
);


ALTER TABLE public.permissions OWNER TO projectsvc;

--
-- TOC entry 189 (class 1259 OID 16425)
-- Name: projects; Type: TABLE; Schema: public; Owner: projectsvc
--

CREATE TABLE public.projects (
    id uuid DEFAULT public.uuid_generate_v4() NOT NULL,
    created_at timestamp with time zone,
    updated_at timestamp with time zone,
    name character varying(255),
    "desc" text,
    to_be_deleted boolean,
    is_being_archived boolean,
    is_archived boolean
);


ALTER TABLE public.projects OWNER TO projectsvc;

--
-- TOC entry 188 (class 1259 OID 16418)
-- Name: roles; Type: TABLE; Schema: public; Owner: projectsvc
--

CREATE TABLE public.roles (
    id uuid DEFAULT public.uuid_generate_v4() NOT NULL,
    name character varying(127),
    project_id uuid
);


ALTER TABLE public.roles OWNER TO projectsvc;

--
-- TOC entry 2171 (class 0 OID 16434)
-- Dependencies: 190
-- Data for Name: members; Type: TABLE DATA; Schema: public; Owner: projectsvc
--

COPY public.members (project_id, user_id, role_id, last_access) FROM stdin;
623be379-ed40-49f3-bdd8-416f8cd0faa6	5d5d0365-4a70-4912-b727-f69309142c7e	5e6a42af-0ad9-4cea-8de2-595590b8240b	2018-05-24 11:14:06.289064+00
73fcb3bf-bc8b-4c8b-801f-8a90d92bf9c2	5d5d0365-4a70-4912-b727-f69309142c7e	e06316e9-8557-4271-bf01-9d7bbd0ea112	2018-05-24 13:03:03.847338+00
f3aced7f-d27f-4694-b5e7-5ed40d4944f7	5d5d0365-4a70-4912-b727-f69309142c7e	d69ab553-f6ff-44a2-a714-007a258f6dfe	2018-05-24 14:19:28.31828+00
2085eb4c-7a94-4cc8-9c46-58f5166d3c82	5d5d0365-4a70-4912-b727-f69309142c7e	edb53a13-bd69-49c0-a91a-08d8a4e00500	2018-05-25 09:49:31.881506+00
f05725ff-7da3-4dbe-83ce-184a585f47df	5d5d0365-4a70-4912-b727-f69309142c7e	85240a0f-37b7-4f54-a25b-4dcc5f387b7b	2018-05-25 09:49:38.312886+00
be69cb8c-45e4-4d80-8d55-419984aa2151	5d5d0365-4a70-4912-b727-f69309142c7e	5f190f3a-299a-4eb8-a066-1897c942b7c2	2018-05-25 09:49:45.455409+00
\.


--
-- TOC entry 2167 (class 0 OID 16397)
-- Dependencies: 186
-- Data for Name: permission_defaults; Type: TABLE DATA; Schema: public; Owner: projectsvc
--

COPY public.permission_defaults (id, name, min_value, max_value, default_value) FROM stdin;
69ceb2e8-489a-47a1-9b4d-cc570240365b	admin_access	0	1	0
01d65977-0774-482b-b192-137b37d35d93	invite_member	0	1	0
ac0fea5c-2fec-4ede-b976-01bc7df3ddc5	change_files	0	1	0
\.


--
-- TOC entry 2168 (class 0 OID 16411)
-- Dependencies: 187
-- Data for Name: permissions; Type: TABLE DATA; Schema: public; Owner: projectsvc
--

COPY public.permissions (id, permission_default_id, role_id, value) FROM stdin;
0d01f0b1-3667-4cf7-ba67-f6ba9a78c13d	69ceb2e8-489a-47a1-9b4d-cc570240365b	5e6a42af-0ad9-4cea-8de2-595590b8240b	1
ef34544a-f14d-4531-97ee-e596ad550a3f	01d65977-0774-482b-b192-137b37d35d93	5e6a42af-0ad9-4cea-8de2-595590b8240b	1
1deee456-56fe-4c8d-9cf9-a6b8aa99098e	ac0fea5c-2fec-4ede-b976-01bc7df3ddc5	5e6a42af-0ad9-4cea-8de2-595590b8240b	1
e2593b33-8c0d-4217-a3b4-d0340e103b05	ac0fea5c-2fec-4ede-b976-01bc7df3ddc5	415c3a3d-33f0-4398-8561-3d0efa64a40d	1
5c86aa9f-6976-43e8-a7d4-e414ef110879	69ceb2e8-489a-47a1-9b4d-cc570240365b	e06316e9-8557-4271-bf01-9d7bbd0ea112	1
ff88bfd7-2556-479a-bc81-34814c6eb6e9	01d65977-0774-482b-b192-137b37d35d93	e06316e9-8557-4271-bf01-9d7bbd0ea112	1
a660c14f-d1ab-4acf-9234-2f70c9724909	ac0fea5c-2fec-4ede-b976-01bc7df3ddc5	e06316e9-8557-4271-bf01-9d7bbd0ea112	1
900f685a-131e-4088-8946-ed30e8b041dd	ac0fea5c-2fec-4ede-b976-01bc7df3ddc5	fa2a7074-009d-4d77-8e9c-00349fbf43fd	1
bce43269-a858-409d-b420-1f15225fd247	69ceb2e8-489a-47a1-9b4d-cc570240365b	d69ab553-f6ff-44a2-a714-007a258f6dfe	1
64959d96-545e-4dbd-b3fe-008cb62dd4da	01d65977-0774-482b-b192-137b37d35d93	d69ab553-f6ff-44a2-a714-007a258f6dfe	1
312547f9-29c9-4c8b-9bfa-d63ca9d56c1e	ac0fea5c-2fec-4ede-b976-01bc7df3ddc5	d69ab553-f6ff-44a2-a714-007a258f6dfe	1
7665e042-fa88-4283-8321-423209c2003a	ac0fea5c-2fec-4ede-b976-01bc7df3ddc5	7a97cd56-6e9f-413e-afa2-35fe951e1420	1
97fee291-6db4-4994-a882-5d9ca9795d3b	69ceb2e8-489a-47a1-9b4d-cc570240365b	edb53a13-bd69-49c0-a91a-08d8a4e00500	1
4cc86a76-2940-463c-90f6-0835984fc558	01d65977-0774-482b-b192-137b37d35d93	edb53a13-bd69-49c0-a91a-08d8a4e00500	1
eaea2f24-b12f-4b5d-85ad-50af59051bc6	ac0fea5c-2fec-4ede-b976-01bc7df3ddc5	edb53a13-bd69-49c0-a91a-08d8a4e00500	1
d4b57abe-8cc9-49b0-b5b5-90f236254e03	ac0fea5c-2fec-4ede-b976-01bc7df3ddc5	a72ed68d-2beb-4915-b19f-e6ae9ce87d92	1
33f0b8ff-7bf7-4ee1-b545-b06aec78b4e3	69ceb2e8-489a-47a1-9b4d-cc570240365b	85240a0f-37b7-4f54-a25b-4dcc5f387b7b	1
a3e3f4d0-d91c-4da5-ab40-f2347dd0e936	01d65977-0774-482b-b192-137b37d35d93	85240a0f-37b7-4f54-a25b-4dcc5f387b7b	1
c749ea6c-32f7-4a18-9b59-7d3ed9adb54a	ac0fea5c-2fec-4ede-b976-01bc7df3ddc5	85240a0f-37b7-4f54-a25b-4dcc5f387b7b	1
8c880a8b-0d88-40de-beda-a58f9402864a	ac0fea5c-2fec-4ede-b976-01bc7df3ddc5	eef36e0c-06e4-47da-93dd-cc37e72ae60f	1
b4eaa062-2985-436c-89e7-c0ab679ef7da	69ceb2e8-489a-47a1-9b4d-cc570240365b	5f190f3a-299a-4eb8-a066-1897c942b7c2	1
f76e8130-fb85-472a-a063-96624b3cf7f4	01d65977-0774-482b-b192-137b37d35d93	5f190f3a-299a-4eb8-a066-1897c942b7c2	1
b6c30161-6410-440f-a26e-a562ab0567c4	ac0fea5c-2fec-4ede-b976-01bc7df3ddc5	5f190f3a-299a-4eb8-a066-1897c942b7c2	1
358a6ed8-0695-4c74-bc2c-2f8e1b39082b	ac0fea5c-2fec-4ede-b976-01bc7df3ddc5	add9c1db-078c-4909-b0b2-fa7e45488d64	1
\.


--
-- TOC entry 2170 (class 0 OID 16425)
-- Dependencies: 189
-- Data for Name: projects; Type: TABLE DATA; Schema: public; Owner: projectsvc
--

COPY public.projects (id, created_at, updated_at, name, "desc", to_be_deleted, is_being_archived, is_archived) FROM stdin;
623be379-ed40-49f3-bdd8-416f8cd0faa6	2018-05-24 11:14:06.279757+00	2018-05-24 11:14:06.289178+00	Trial	No description available.	f	f	f
73fcb3bf-bc8b-4c8b-801f-8a90d92bf9c2	2018-05-24 13:03:03.839467+00	2018-05-25 09:51:45.315251+00	Trial_2	No description available.	f	f	f
f3aced7f-d27f-4694-b5e7-5ed40d4944f7	2018-05-24 14:19:28.307943+00	2018-05-25 09:52:00.286908+00	Trial_3	No description available.	f	f	f
be69cb8c-45e4-4d80-8d55-419984aa2151	2018-05-25 09:49:45.446222+00	2018-05-25 10:00:47.661636+00	Trial_6	No description available.	t	f	f
f05725ff-7da3-4dbe-83ce-184a585f47df	2018-05-25 09:49:38.305016+00	2018-05-25 10:01:09.661683+00	Trial_5	No description available.	t	f	f
2085eb4c-7a94-4cc8-9c46-58f5166d3c82	2018-05-25 09:49:31.87392+00	2018-05-25 10:01:26.179765+00	Trial_4	No description available.	t	f	f
\.


--
-- TOC entry 2169 (class 0 OID 16418)
-- Dependencies: 188
-- Data for Name: roles; Type: TABLE DATA; Schema: public; Owner: projectsvc
--

COPY public.roles (id, name, project_id) FROM stdin;
415c3a3d-33f0-4398-8561-3d0efa64a40d	member	623be379-ed40-49f3-bdd8-416f8cd0faa6
5e6a42af-0ad9-4cea-8de2-595590b8240b	owner	623be379-ed40-49f3-bdd8-416f8cd0faa6
fa2a7074-009d-4d77-8e9c-00349fbf43fd	member	73fcb3bf-bc8b-4c8b-801f-8a90d92bf9c2
e06316e9-8557-4271-bf01-9d7bbd0ea112	owner	73fcb3bf-bc8b-4c8b-801f-8a90d92bf9c2
7a97cd56-6e9f-413e-afa2-35fe951e1420	member	f3aced7f-d27f-4694-b5e7-5ed40d4944f7
d69ab553-f6ff-44a2-a714-007a258f6dfe	owner	f3aced7f-d27f-4694-b5e7-5ed40d4944f7
a72ed68d-2beb-4915-b19f-e6ae9ce87d92	member	2085eb4c-7a94-4cc8-9c46-58f5166d3c82
edb53a13-bd69-49c0-a91a-08d8a4e00500	owner	2085eb4c-7a94-4cc8-9c46-58f5166d3c82
eef36e0c-06e4-47da-93dd-cc37e72ae60f	member	f05725ff-7da3-4dbe-83ce-184a585f47df
85240a0f-37b7-4f54-a25b-4dcc5f387b7b	owner	f05725ff-7da3-4dbe-83ce-184a585f47df
add9c1db-078c-4909-b0b2-fa7e45488d64	member	be69cb8c-45e4-4d80-8d55-419984aa2151
5f190f3a-299a-4eb8-a066-1897c942b7c2	owner	be69cb8c-45e4-4d80-8d55-419984aa2151
\.


--
-- TOC entry 2049 (class 2606 OID 16438)
-- Name: members members_pkey; Type: CONSTRAINT; Schema: public; Owner: projectsvc
--

ALTER TABLE ONLY public.members
    ADD CONSTRAINT members_pkey PRIMARY KEY (project_id, user_id);


--
-- TOC entry 2037 (class 2606 OID 16410)
-- Name: permission_defaults permission_defaults_name_key; Type: CONSTRAINT; Schema: public; Owner: projectsvc
--

ALTER TABLE ONLY public.permission_defaults
    ADD CONSTRAINT permission_defaults_name_key UNIQUE (name);


--
-- TOC entry 2039 (class 2606 OID 16408)
-- Name: permission_defaults permission_defaults_pkey; Type: CONSTRAINT; Schema: public; Owner: projectsvc
--

ALTER TABLE ONLY public.permission_defaults
    ADD CONSTRAINT permission_defaults_pkey PRIMARY KEY (id);


--
-- TOC entry 2042 (class 2606 OID 16416)
-- Name: permissions permissions_pkey; Type: CONSTRAINT; Schema: public; Owner: projectsvc
--

ALTER TABLE ONLY public.permissions
    ADD CONSTRAINT permissions_pkey PRIMARY KEY (id);


--
-- TOC entry 2047 (class 2606 OID 16433)
-- Name: projects projects_pkey; Type: CONSTRAINT; Schema: public; Owner: projectsvc
--

ALTER TABLE ONLY public.projects
    ADD CONSTRAINT projects_pkey PRIMARY KEY (id);


--
-- TOC entry 2045 (class 2606 OID 16423)
-- Name: roles roles_pkey; Type: CONSTRAINT; Schema: public; Owner: projectsvc
--

ALTER TABLE ONLY public.roles
    ADD CONSTRAINT roles_pkey PRIMARY KEY (id);


--
-- TOC entry 2043 (class 1259 OID 16424)
-- Name: idx_name_pid; Type: INDEX; Schema: public; Owner: projectsvc
--

CREATE UNIQUE INDEX idx_name_pid ON public.roles USING btree (name, project_id);


--
-- TOC entry 2040 (class 1259 OID 16417)
-- Name: idx_pdid_rid; Type: INDEX; Schema: public; Owner: projectsvc
--

CREATE UNIQUE INDEX idx_pdid_rid ON public.permissions USING btree (permission_default_id, role_id);


-- Completed on 2018-05-25 15:00:00 UTC

--
-- PostgreSQL database dump complete
--

\connect template1

SET default_transaction_read_only = off;

--
-- PostgreSQL database dump
--

-- Dumped from database version 9.6.9
-- Dumped by pg_dump version 10.3

-- Started on 2018-05-25 15:00:00 UTC

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 2119 (class 0 OID 0)
-- Dependencies: 2118
-- Name: DATABASE template1; Type: COMMENT; Schema: -; Owner: postgres
--

COMMENT ON DATABASE template1 IS 'default template for new databases';


--
-- TOC entry 1 (class 3079 OID 12390)
-- Name: plpgsql; Type: EXTENSION; Schema: -; Owner: 
--

CREATE EXTENSION IF NOT EXISTS plpgsql WITH SCHEMA pg_catalog;


--
-- TOC entry 2121 (class 0 OID 0)
-- Dependencies: 1
-- Name: EXTENSION plpgsql; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION plpgsql IS 'PL/pgSQL procedural language';


-- Completed on 2018-05-25 15:00:00 UTC

--
-- PostgreSQL database dump complete
--

-- Completed on 2018-05-25 15:00:00 UTC

--
-- PostgreSQL database cluster dump complete
--

