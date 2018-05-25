--
-- PostgreSQL database cluster dump
--

-- Started on 2018-05-24 12:00:00 UTC

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

-- Started on 2018-05-24 12:00:00 UTC

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


-- Completed on 2018-05-24 12:00:00 UTC

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

-- Started on 2018-05-24 12:00:00 UTC

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
\.


--
-- TOC entry 2170 (class 0 OID 16425)
-- Dependencies: 189
-- Data for Name: projects; Type: TABLE DATA; Schema: public; Owner: projectsvc
--

COPY public.projects (id, created_at, updated_at, name, "desc", to_be_deleted, is_being_archived, is_archived) FROM stdin;
623be379-ed40-49f3-bdd8-416f8cd0faa6	2018-05-24 11:14:06.279757+00	2018-05-24 11:14:06.289178+00	Trial	No description available.	f	f	f
\.


--
-- TOC entry 2169 (class 0 OID 16418)
-- Dependencies: 188
-- Data for Name: roles; Type: TABLE DATA; Schema: public; Owner: projectsvc
--

COPY public.roles (id, name, project_id) FROM stdin;
415c3a3d-33f0-4398-8561-3d0efa64a40d	member	623be379-ed40-49f3-bdd8-416f8cd0faa6
5e6a42af-0ad9-4cea-8de2-595590b8240b	owner	623be379-ed40-49f3-bdd8-416f8cd0faa6
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


-- Completed on 2018-05-24 12:00:00 UTC

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

-- Started on 2018-05-24 12:00:00 UTC

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


-- Completed on 2018-05-24 12:00:00 UTC

--
-- PostgreSQL database dump complete
--

-- Completed on 2018-05-24 12:00:00 UTC

--
-- PostgreSQL database cluster dump complete
--

