INSERT INTO Languages
(ID, TextLanguage)
VALUES(1, 'Enska');

INSERT INTO Languages
(ID, TextLanguage)
VALUES(2, 'Íslenska');

SET IDENTITY_INSERT Subtitles ON
INSERT INTO Subtitles
(ID, U_ID, L_ID, Title, Category)
VALUES(1, 1, 1, 'Die Hard', 'Spenna');

INSERT INTO Subtitles
(ID, U_ID, L_ID, Title, Category)
VALUES(2, 2, 1, 'Borat', 'Gaman');
SET IDENTITY_INSERT Subtitles OFF

SET IDENTITY_INSERT Requests ON
INSERT INTO Requests
(ID, U_ID, S_ID)
VALUES(1, 1, 1);

INSERT INTO Requests
(ID, U_ID, S_ID)
VALUES(2, 2, 2);
SET IDENTITY_INSERT Requests OFF

SET IDENTITY_INSERT SubtitleLines ON
INSERT INTO SubtitleLines
(ID, S_ID, Start, Duration, SLText)
VALUES(1, 1, '00:30:30', '00:00:10','Yippikayyay motherfucker')

INSERT INTO SubtitleLines
(ID, S_ID, Start, Duration, SLText)
VALUES(2, 2, '00:20:30', '00:00:20', 'Look, there is a woman in a car! Can we follow her and maybe make a sexy time with her? ');
SET IDENTITY_INSERT SubtitleLines OFF

SET IDENTITY_INSERT LineTranslations ON
INSERT INTO LineTranslations
(ID, SL_ID, U_ID, L_ID, Text)
VALUES(1, 1, 1, 1, 'Yippi...');

INSERT INTO LineTranslations
(ID, SL_ID, U_ID, L_ID, Text)
VALUES(2, 2, 2, 1, 'Look ...');
SET IDENTITY_INSERT LineTranslations OFF