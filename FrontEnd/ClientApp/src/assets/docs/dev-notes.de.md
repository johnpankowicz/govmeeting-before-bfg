<h1> Behandeln Sie neue Transkripte </h1>
<p> Einige Städte erstellen Abschriften von Versammlungen. Auf diese Weise können wir das Transkribieren des Meetings selbst überspringen. Aber es stellt ein anderes Problem dar. Transkripte werden nicht in einem Standardformat vorliegen. </p>

<p> Unsere Software muss: </p>

<ul>
<li> Extrahieren Sie die Informationen. </li>
<li> Fügen Sie Tags hinzu, mit denen die Informationen einfach verwendet werden können. </li>
</ul>
<p> Informationen, die normalerweise in einem Transkript enthalten sind und die wir extrahieren möchten, sind: </p>

<ul>
<li> Besprechungsinformationen: Zeit, Ort, ob es sich um eine besondere Besprechung handelt. </li>
<li> Anwesende Beamte </li>
<li> Abschnittsüberschriften </li>
<li> Name jedes Sprechers und was sie gesagt haben. </li>
</ul>
<p> Wenn keine Abschnittsüberschriften vorhanden sind, sollte die Software intelligent genug sein, um zu bestimmen, wo allgemeine Abschnitte beginnen: </p>

<ul>
<li> Rollenaufruf </li>
<li> Aufruf </li>
<li> Ausschussberichte </li>
<li> Einführung von Rechnungen </li>
<li> Beschlüsse </li>
<li> Öffentlicher Kommentar </li>
</ul>
<p> Wir werden sehen müssen, wie gut wir auch Abstimmungsergebnisse zu Gesetzentwürfen und Beschlüssen extrahieren können. Manchmal werden die Ergebnisse durch Sätze wie "Die Ayes haben es" angezeigt. In anderen Fällen findet eine formelle Abstimmung statt, bei der der Name jedes Beamten vorgelesen wird und die Person mit "Ja" oder "Nein" antwortet. </p>

<p> Überflüssige Informationen müssen entfernt werden. Zum Beispiel: Wiederholen von Kopf- oder Fußzeilen, Zeilennummern und Seitenzahlen. </p>

<p> Es ist zu hoffen, dass allgemeiner Code geschrieben werden kann, der Informationen aus neuen Transkripten extrahieren kann, die es noch nie gegeben hat. Bis dahin muss jedoch neuer Code geschrieben werden, um bestimmte Fälle zu behandeln. </p>

<p> Da normalerweise nur größere Städte Transkripte erstellen: </p>

<ul>
<li> Die meiste Zeit werden wir uns mit Aufzeichnungen von Besprechungen befassen. </li>
<li> In einer größeren Stadt gibt es eher Computerprogrammierer, die in der Lage sind, solchen Code zu schreiben. </li>
</ul>
<p> Wir könnten einen Plug-In-Mechanismus erstellen, mit dem Module hinzugefügt werden können, die die Extraktion durchführen. Wir könnten zulassen, dass die Plugins in vielen verschiedenen Sprachen geschrieben werden: Python, Java, PHP, Ruby - zusätzlich zu den Sprachen, in denen das System derzeit geschrieben ist: Typescript und C #. </p>

<p> Derzeit behandelt die Software nur einen Fall, Philadelphia, PA USA. Die Projektbibliothek "Backend \ ProcessMeetings \ ProcessTranscripts_lib" enthält Code zur Verarbeitung von Transkripten. </p>

<p> Die Klasse "Specific_Philadelphia_PA_USA" ruft einige Allzweckroutinen auf, um Transkripte für Philadelphia zu verarbeiten. </p>

<p> Es gibt eine Stub-Klasse "Specific_Austin_TX_USA", die für die Verarbeitung eines Transkripts in Austin, TX, vorgesehen ist. Vielleicht möchte jemand versuchen, diesen Code zu vervollständigen. Im Ordner Testdata befindet sich ein Testprotokoll. Aber es ist wahrscheinlich am besten, das Neueste von ihrer Website zu erhalten: <a href="https://www.austintexas.gov/department/city-council/council/council_meeting_info_center.htm">Austin, TX City Council</a> </p>
<h1> Ändern des Client-Dashboards </h1><h2> Fügen Sie eine Karte für eine neue Funktion hinzu </h2>
<ul>
<li> Wechseln Sie an einer Terminal-Eingabeaufforderung in den Ordner: FrontEnd / ClientApp </li>
<li> Geben Sie Folgendes ein: ng Komponente Ihrer Funktion generieren </li>
<li> Fügen Sie Ihre Funktionalität zu den Dateien in: FrontEnd / ClientApp / src / app / your-feature hinzu </li>
<li> Fügen Sie ein neues gm-small-card- oder gm-large-card-Element in app / dash-main / dash-main.html ein. </li>
<li> Ändern Sie das Symbol, die Symbolfarbe, den Titel usw. des Kartenelements. </li>
<li> Wenn Sie Zugriff auf den Namen des aktuell ausgewählten Standorts und der Agentur in Ihrem Controller benötigen: 
<ul>
<li> Fügen Sie Ihrem Feature-Element die Standort- und Agentureingabeattribute hinzu </li>
<li> Fügen Sie Ihrem Controller Standort- und Agentureigenschaften @Input hinzu. </li>
</ul></li>
</ul>
<p> (Siehe die anderen Beispiele in dash-main.html) </p>
<h2> Karten neu anordnen </h2><ol>
<li> Öffnen Sie die Datei: FrontEnd / ClientApp / src / app / dash-main / dash-main.html. </li>
<li> Ändern Sie die Kartenpositionen, indem Sie die Position der gm-small-card- oder gm-large-card-Elemente in dieser Datei ändern. </li></ol><h1> Protokollierung </h1>
<p> Die Protokolldateien für WebApp und WorkflowApp befinden sich im Ordner "logs" im Stammverzeichnis der Lösung. </p>

<ul>
<li> "nlog-all- (date) .log" enthält alle Protokollmeldungen einschließlich des Systems. </li>
<li> Die Datei "nlog-own- (date) .log" enthält nur die Anwendungsnachrichten. </li>
</ul>
<p> Am Anfang vieler Komponentendateien in ClientApp wird eine Konstante "NoLog" definiert. Ändern Sie den Wert von true in false, um die Konsolenprotokollierung nur für diese Komponente zu aktivieren. </p>
<h1> Skripte erstellen </h1>
<p> Powershell-Build-Skripte finden Sie in Utilities / PsScripts </p>
<h2> BuildPublishAndDeploy.ps1 </h2>
<p> Dieses Skript ruft viele der anderen Skripte auf, um eine Produktionsversion zu erstellen, und stellt sie bereit. </p>

<ul>
<li> Build-ClientApp.ps1 - Erstellen Sie Produktionsversionen von ClientApp </li>
<li> Publish-WebApp.ps1 - Erstellen Sie einen "Publish" -Ordner von WebApp </li>
<li> Copy-ClientAssets.ps1 - Kopieren Sie ClientApp-Assets in den WebApp-Ordner wwwroot </li>
<li> Deploy-PublishFolder.ps1 - Stellen Sie den Veröffentlichungsordner auf einem Remote-Host bereit </li>
<li> Erstellen Sie die Datei README.md für Gethub aus den Dokumentationsdateien </li>
</ul>
<p> Deploy-PublishFolder.ps1 stellt die Software über FTP auf govmeeting.org bereit. Die FTP-Anmeldeinformationen befinden sich in der Datei appsettings.Development.json im Ordner _SECRETS. Es enthält FTP und andere Geheimnisse zur Verwendung in der Entwicklung. Unten ist das Format dieser Datei: </p>
<pre> <code>{ "ExternalAuth": { "Google": { "ClientId": "your-client-id", "ClientSecret": "your-client-secret" } }, "ReCaptcha": { "SiteKey": "your-site-key", "Secret": "your-secret" }, "Ftp": { "username": "your-username", "password": "your-password", "domain": "your-domain" } }</code> </pre>