﻿CREATE DATABASE warehouse;

CREATE TABLE public.pallets (
	id int4 NOT NULL GENERATED BY DEFAULT AS IDENTITY( INCREMENT BY 1 MINVALUE 1 MAXVALUE 2147483647 START 1 CACHE 1 NO CYCLE),
	barcode text UNIQUE NOT NULL,
	CONSTRAINT "PK_pallet" PRIMARY KEY (id)
);

CREATE TABLE public.boxes (
	id int4 NOT NULL GENERATED BY DEFAULT AS IDENTITY( INCREMENT BY 1 MINVALUE 1 MAXVALUE 2147483647 START 1 CACHE 1 NO CYCLE),
	barcode text NOT NULL,
	pallet_id int4 NOT NULL,
	owner_id int4 NULL,
	UNIQUE (barcode),
	CONSTRAINT "PK_boxes" PRIMARY KEY (id),
	CONSTRAINT "FK_boxes_pallet_pallet_id" FOREIGN KEY (pallet_id) REFERENCES public.pallets(id) ON DELETE CASCADE,
	CONSTRAINT "FK_boxes_boxes_owner_id" FOREIGN KEY (owner_id) REFERENCES public.boxes(id) ON DELETE SET NULL
);
