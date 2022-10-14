Feature: Attractie

Scenario: BestaatAl
    Given attractie Draaimolen bestaat
    When attractie Draaimolen wordt toegevoegd
    Then moet er een error 403 komen

Scenario: BestaatNiet
    Given attractie test bestaatniet
    When attractie test wordt verwijderd
    Then moet er een error 404 komen

    Scenario: ZoekOp
        Given attractie Draaimolen bestaat
        When attractie Draaimolen wordt opgezocht
        Then moet er een error 200 komen
