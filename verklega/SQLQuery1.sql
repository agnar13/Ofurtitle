INSERT INTO Subtitles
(U_ID, L_ID, Title, Category)
VALUES(1, 1, 'Die Hard', 'Spenna');

INSERT INTO Subtitles
(U_ID, L_ID, Title, Category)
VALUES(2, 2, 'Borat', 'Gaman');

INSERT INTO Languages
(ID, TextLanguage)
VALUES(1, 'Enska');

INSERT INTO Languages
(ID, TextLanguage)
VALUES(2, 'Íslenska');

INSERT INTO Requests
(U_ID, S_ID)
VALUES(1, 1);

INSERT INTO Requests
(U_ID, S_ID)
VALUES(2, 2);

INSERT INTO SubtitleLines
(S_ID, Start, Duration)
VALUES(1, '00:30:30', '00:00:10')

INSERT INTO SubtitleLines
(S_ID, Start, Duration)
VALUES(2, '00:20:30', '00:00:20');

INSERT INTO LineTranslations
(SL_ID, U_ID, L_ID, Text)
VALUES(1, 1, 1, 'Yippikayyay motherfucker');

INSERT INTO LineTranslations
(SL_ID, U_ID, L_ID, Text)
VALUES(2, 2, 1, 'Look, there is a woman in a car! Can we follow her and maybe make a sexy time with her? ');




