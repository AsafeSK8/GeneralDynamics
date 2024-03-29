USE [GDAI]
GO
SET IDENTITY_INSERT [dbo].[GDRole] ON 

INSERT [dbo].[GDRole] ([Id], [Code], [Description]) VALUES (1, N'ADM', N'Administrador')
INSERT [dbo].[GDRole] ([Id], [Code], [Description]) VALUES (2, N'TEST', N'Prueba rol')
INSERT [dbo].[GDRole] ([Id], [Code], [Description]) VALUES (5, N'ONE', N'Prueba listado rol')
INSERT [dbo].[GDRole] ([Id], [Code], [Description]) VALUES (6, N'TWO', N'Prueba listado rol')
INSERT [dbo].[GDRole] ([Id], [Code], [Description]) VALUES (7, N'AAA', N'Prueba listado rol')
INSERT [dbo].[GDRole] ([Id], [Code], [Description]) VALUES (9, N'USER', N'Usuario')
SET IDENTITY_INSERT [dbo].[GDRole] OFF
SET IDENTITY_INSERT [dbo].[GDUser] ON 

INSERT [dbo].[GDUser] ([Id], [Name], [LastName], [Email], [Phone], [UserName], [Password], [Token], [RoleId]) VALUES (1, N'Asafe', N'Olimpio', N'asafe.olimpio@babelgroup.com', N'697849958', N'aolimpio', N'Abcd,1', N'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6ImFvbGltcGlvIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvZW1haWxhZGRyZXNzIjoiYXNhZmUub2xpbXBpb0BiYWJlbGdyb3VwLmNvbSIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2dpdmVubmFtZSI6IkFzYWZlIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvc3VybmFtZSI6Ik9saW1waW8iLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJBRE0iLCJleHAiOjE2NjU3NDc1NDEsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjQ0MzE5LyIsImF1ZCI6Imh0dHBzOi8vbG9jYWxob3N0OjQ0MzE5LyJ9.ANJL9NbDfcrtuq9jt4R3bzMQo76yZp3CZHQ8Dtu1MUE', 1)
INSERT [dbo].[GDUser] ([Id], [Name], [LastName], [Email], [Phone], [UserName], [Password], [Token], [RoleId]) VALUES (2, N'MARIA BEGOÑA', N'BELLO DEL VALLE', N'pdwe47le6@mail.com', N'661329094', N'mariabegona_55', N'Abcd,2', N'eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6Im1hcmlhYmVnb8OxYV81NSIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2VtYWlsYWRkcmVzcyI6InBkd2U0N2xlNkBtYWlsLmNvbSIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL2dpdmVubmFtZSI6Ik1BUklBIEJFR0_DkUEiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9zdXJuYW1lIjoiQkVMTE8gREVMIFZBTExFIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9yb2xlIjoiVVNFUiIsImV4cCI6MTY2NTc0NTk5NywiaXNzIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NDQzMTkvIiwiYXVkIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6NDQzMTkvIn0.tvTaFwiGM19w_0p7ssHEDSchRizR0NZxKj1AW8BqM5E', 9)
INSERT [dbo].[GDUser] ([Id], [Name], [LastName], [Email], [Phone], [UserName], [Password], [Token], [RoleId]) VALUES (3, N'FELIPE', N'JIMENEZ GALLEGO', N'rm7xsele3@lycos.nl', N'655585109', N'felipe_67', N'Abcd,3', N'', 5)
INSERT [dbo].[GDUser] ([Id], [Name], [LastName], [Email], [Phone], [UserName], [Password], [Token], [RoleId]) VALUES (4, N'MARCOS', N'ESCRIBANO CACERES', N'4i2rtgp5eh@earthling.net', N'792580516', N'marcos_81', N'Abcd,4', N'', 5)
INSERT [dbo].[GDUser] ([Id], [Name], [LastName], [Email], [Phone], [UserName], [Password], [Token], [RoleId]) VALUES (5, N'ISABEL', N'DIAZ HERNANDEZ', N'qmhbredkz@hotmail.com', N'615943943', N'isabel_53', N'Abcd,5', NULL, 9)
SET IDENTITY_INSERT [dbo].[GDUser] OFF
