# Baze-MVC

Mehanizam pub/sub nije implementiran u Redisu, ali je umesto toga implementirano kesiranje nedavno pregledanih filmova, 
sa svrhom da se vec pregledani filmovi brze ucitavaju povlacenjem iz Redisa. Takodje je dodato da za skoro pogledane filomove 
koji se nalaze u kesu, moguce je videti slicne filmove po zanru (kombinacija Redis i Neo4j).