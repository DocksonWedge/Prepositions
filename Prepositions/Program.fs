// Learn more about F# at http://fsharp.org

open System


type Form = 
    | Dativ
    | Akkusativ
    | Wechsel
    | Falsch


//let checkAnswer  answer=
let dative = Set.ofList ["ab"; "ausser"; "zu"; "nach"; "bei"; "von"; "aus"; "mit"; "seit"; "gegenuber"]
let wechsel = Set.ofList ["in"; "an"; "auf";  "neben"; "hinter"; "uber"; "unter"; "vor"; "zwischen"]
let akkusativ = Set.ofList ["bis"; "durch"; "ohne"; "fur"; "gegen"; "um"]
let prepositions = Set.toArray(Set.unionMany(seq[dative; akkusativ; wechsel]))

let getType (prep:String) = 
    match prep.ToLower() with
    | p when dative.Contains(p) -> Dativ
    | p when akkusativ.Contains(p) -> Akkusativ
    | p when wechsel.Contains(p) -> Wechsel
    | _ -> Falsch

let getRandomString (arr:String[]) =  
  let rnd = System.Random()  
  arr.[rnd.Next(arr.Length)]

[<EntryPoint>]
let main argv =

    let mutable running = true
    //use tail recursion to be more functional?
    while running do 
        let question = getRandomString prepositions
        printfn "%s" question
        let expectedType = (getType question).ToString().ToLower()
        let input = System.Console.ReadLine().ToLower()
        running <- not(String.IsNullOrWhiteSpace input)

        let evaluateResult (i: String) = 
            match i with
            | i when i.Equals expectedType -> "Richtig"
            | i when i.Contains expectedType -> "Falsche Schreibung"
            | i when expectedType.Contains i -> "Falsche Schreibung"
            | _ -> String.Format("**Falsch: {0}", expectedType)

        printfn "%s\n----" (evaluateResult input)
        
    0 // return an integer exit code
