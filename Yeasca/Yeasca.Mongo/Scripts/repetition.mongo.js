conn = new Mongo();
db = conn.getDB("Yeasca");
repetition = {"Valeur": "Bis"}
db.ParametreMongo.save(repetition);
repetition = {"Valeur": "Ter"};
db.ParametreMongo.save(repetition);
repetition = {"Valeur": "Quarter"};
db.ParametreMongo.save(repetition);
repetition = {"Valeur": "Quinquies"};
db.ParametreMongo.save(repetition);
