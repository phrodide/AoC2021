using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2021.Days
{
    internal class Day10
    {
        public Day10()
        {

        }

        public string Clean(string input)
        {
            string output = input.Replace("[]", "").Replace("()", "").Replace("<>", "").Replace("{}", "");
            if (output==input) return input;
            else return Clean(output);
        }

        public string SolvePart1()
        {
            var lines = data.Replace("\r\n", "\n").Split('\n');
            int counter = 0;
            foreach (var line in lines)
            {
                var cleaned = Clean(line);
                foreach (var item in cleaned)
                {
                    bool found = false;
                    switch (item)
                    {
                        case ')':
                            counter += 3;
                            found = true;
                            break;
                        case ']':
                            counter += 57;
                            found = true;
                            break;
                        case '}':
                            counter += 1197;
                            found = true;
                            break;
                        case '>':
                            counter += 25137;
                            found = true;
                            break;
                    }
                    if (found) break;
                }
            }
            return counter.ToString();
        }
        public string SolvePart2()
        {
            var lines = data.Replace("\r\n", "\n").Split('\n');
            List<long> scores = new List<long>();
            foreach (var line in lines)
            {
                var cleaned = Clean(line);
                bool found = false;
                foreach (var item in cleaned)
                {
                    switch (item)
                    {
                        case ')':
                            found = true;
                            break;
                        case ']':
                            found = true;
                            break;
                        case '}':
                            found = true;
                            break;
                        case '>':
                            found = true;
                            break;
                    }
                    if (found) break;
                }
                if (!found)
                {
                    //this is an incomplete line.
                    long score = 0;
                    var incomplete = cleaned.Reverse();
                    foreach (var item in incomplete)
                    {
                        score = score*5 + (item=='(' ? 1 : item=='[' ? 2 : item=='{' ? 3 : 4);
                    }
                    scores.Add(score);
                }
            }
            int take = (scores.Count-1)/2;
            return scores.OrderBy(x => x).Skip(take).First().ToString();
        }

        public static string tdata = @"[({(<(())[]>[[{[]{<()<>>
[(()[<>])]({[<{<<[]>>(
{([(<{}[<>[]}>{[]{[(<()>
(((({<>}<{<{<>}{[]{[]{}
[[<[([]))<([[{}[[()]]]
[{[{({}]{}}([{[{{{}}([]
{<[[]]>}<{[{[{[]{()[[[]
[<(<(<(<{}))><([]([]()
<{([([[(<>()){}]>(<<{{
<{([{{}}[<[[[<>{}]]]>[]]";

        public static string data = @"([<([{{<<(<<{[<><>)}<([][])<<>{}>>>{[{(){}}(()[])]<{()()}<()()>>}>([<<{}<>>(<><>)><[{}[]](<>())>][
({{<<(<<<<{{[({}{})<[]()>]<([]{}){[]<>}>}{([{}[]]}}}>({(((()()))<{<>()}[<>()]>)[{(<>{})(()[])}[{[]
[{{[([<({[([<{[]}({}<>)>]{(([]())[()[]]){<[]<>>[<><>]}}>{<[(()<>){[]<>}][[<>{}]<()()>]>[[[<>[]][()
{(<<{([([<(<{(<>[])<()<>>}>){[[<<>()>[<>[]]]{[{}{}]{[]<>}}]{<{{}<>}{{}()}>{[()()]<[]<>>})}>[{({[{}
<{<<<<<{{{({<<[][]>[()[]]><<(){}>([]{})>}{{<[]{}><{}[]>}{<{}{}>({}<>)}}){([(()[]){[]()}][<[]()><[]{}>])
<{({{<{{{<(({(<>)({}[])}{<()>{(){}}})[[(<><>)[[]]]<[{}[]]{<>()}>])>{<<([{}()]{[][]})({{}{}}{()})><
<([[<<[{[{({{({}<>)[(){}]}{[{}<>]{[]{}}}}({[()()]<<><>>}(<{}[]><[]{}>)))}<({<<[]<>>({}[])>
[[{<{{[{<{<[{[()[]]<()()>}][<(<>[]){<>()}>[{<><>}(<>())]]><[{{{}<>}<{}{}>}{[{}[]][[][]]}]<[([]<>){[]{}}]<(
[({[<([[{<[{{(()[])<[]{}>}<[{}<>>{[][]}>}(<<[]{}>([]<>)>[([]){()[]}])]<[[[[]()][<>{}]]<(<>{})((
<{[[{{{[([([{[<>()]{[]{}}}<{[][]}[<>()]>](<<<><>>{()()}>))<<{{<><>}[{}()]}{(<><>)[<>{}]}>>])]{[<<[<([]
[[(<<<({[(((([<>()]([]{}))((<>){[]()})})[[<[[]<>]<{}<>>>[<()()>[()<>]]][[[<>{}]{[][]}]]])<<(({{}[
{{{[[<<{{[{<[[{}()]]>[{[<>[]]([][])}<{{}()}{{}[]}>]}([[{()}{(){}}]([<>{}]<{}<>>)][{{[]<>}}[{{}{}}[{
{[{[(<{{([{<<<[][]><()<>>>[{{}()}{[]<>}]>({<{}()>[()[]]}<<()[]>([][])>)}{[([[]{}][<><>]){{{}[]}<
{[({<{[<{{[(({()<>}({}[]))([()]{<>[]}))({{[]{}}<<>[]>>)]{((<()[]>{(){}})[{[]<>}])}}}<[[<<<(){}><()
[[[[[{{[{<<[<<{}<>>>{<()()><{}[]>}]>(<([()<>}<<>[]>)(((){})<()[]>)><({{}{}}<[]()>)<{{}<>}(
{{<<[<{(<[([(<{}<>>[[][]])[(()())]]{[([][])({}<>)]<(<>{}){[][]}>})([{(<>{})<()<>>}])]>(([[[(<>{})](<[]
<<([{<(<{({<{({}<>)<()()>}{<<>[]>[{}{}]}>(<((){})>(<[]()>[<><>]))}<<(([])[()])<([][])(()<>)>>>)([{([<>{}]([]<
{[[{{[[({{[([{<>[]}[{}[]]]){(<[]{}>[()()]){[<>{}][<>]>}]}{<{<<{}()>({}())>}{[[<>[]]{<>{}}]<[{}[]]{{}{}}>}
{<[[{({<{<[<(<(){}><<>{}>)>{[[(){}]]{({}())({}[])}}]{[{{(){}}}]}>}(<{{{([]{})[{}<>]}(<<>{}>)}(
<[[<(<([[({(<{<>()}([][])><(<>[])[<>{}]>)((<<>()>[[]()])<{()<>}<()()>>)}>[[({{{}<>}[[]<>]}
<{([{({[[({([[{}()]<()()>][<()[]>[[]<>]]}([<(){}>{<><>}][<{}<>><<>{}>])}[(([<>[]][<>[]])){
<{[<(<(<[[[{[{<>{}}]((())({}()))}][(([{}<>]{{}{}}))[{[[]()]((){})}<[<>[]]([][])>]]]][[((({
({[<<([[[<[[<(<>{}){{}[]}>[((){})[<><>]]][{{<>{}}{(){}}}(<[]{}}({}<>))]]{((<{}[]>({}<>))<<<>{}>[[]{}]>)[((
{[<<[([[([{<<[{}{}]({}[])><<{}<>)(<>[])>>[([()()]{<><>}){[[]{}]{(){}}}]}{<([<>]([]())){<(){}>{()<>}}><<{
[<[[[[({{([{{(<>[])<<><>>}<{[]{}}<<>>>}(<{[]{}}<<>()>>)][{(<<><>><()[]>){[{}()]>}<[{{}{}}{<>[]}]>]
{<{[<(([{{({[<<>[]><{}[]>]<{<>{}}{()()}>}([<{}{}>{[]<>}]{<{}<>><[]>}))<([([]<>){[]{}}]<{[]<>}
[<<(({<<[{[([[<>]<()[]>]{[[]<>]{()<>}}){{<<><>>{[]<>]}[({}<>){()<>}]}](<{{()[]}}({{}[]}<{}[
({(<{<{<([([[((){})<{}<>>]<[{}[]](<>{})>])])<<[{<<<>[]>>}<[(<><>){<>{}}][(()[])(<>{})]>]<(<{<>{}}
([<{<<[([({<<<()()>(())><[[]<>]{<>[]}>>([(()())]{[[]()]<{}<>>})})[<[[(()<>){<>{}}]][<(<><>){<>[]}>]><<([{}
([[<[[[({[<<[[{}()](()<>)]<{()[]}>>{<[{}<>]<()[]>><<[][]>>}>]<(<<[{}<>){<>}>{<[]()>[[][]]}>)>}({
(<[(<[([<{<{<[(){}][<>()]><(<>())(()<>)>}{{[{}>([]<>)}[<<>{}>(()())]}>[{([<><>]<()()>)}{([()]{()<
({{{{<[{{<{<<(()[])>{[(){}]{[]{}}}>}{{([<><>][<><>])<(()<>)[[][]]>}<<(()<>)><{<>()}>>}>}}][(<{(<[{<><>}[{}[
<{[{((([({{({<()()>(())}){([()<>]((){}))<(()<>)[(){}]>}}{[{([]())((){})}[{[]<>>{<>{}}]]<<<()[]><()<>>>((()()
{([({[(<[<([{[<>[]][()<>]}{[(){}]{{}<>}}][<<{}()>>[<<>()>[()}]])<((({}{})[[]<>]))<{[[]{}]([]<>)}>>><{
([[[{<[<([{(<<<>()>({}<>)><[<>{}]>)[[<<><>><()[]>](<()<>>(<>[]))]}([({()()}([]())){<[]{}>[[]<>]}][<
<<((<({{(<((<({}[])(<>())>{({}[])({}<>)}))<<<([]<>)[()[]]>{<()<>>[[]<>]}>>)({(<[<>{}]((){})><{()()}[{}(
{({([(({{<{{{<[]()>{{}[]})[[(){}](()<>)]}(([[]{}]{()<>}))}>{(<[{()()}]{[<>[]][()()]}>{{<()()
{[(<{<[<<[{<<<<>{}>>[[[]{}]<<>()>]>((<[]{}>{{}()}))}{({[{}()](()<>)}<<[][]>[{}<>]>)({[{}[]]}<<{}[]
{{{<{<{[[{([{{[]()}}({()[]}(<>))]<((<><>)({}{}))>)}]]{[({<(<{}[]>{<>{}})((()<>)([]()))>})[<[[{<>{}}<<><>>]<{<
[[<{<<[(<<<([<{}()>[{}()]](<{}[]>[{}()]))[([[]<>][{}{}])[<[]()><[]()>]]><[[[[]<>]({}{})][{[]}{<>[]}]][<[<>
(<<<<(<<[<[(({[]()}[[]<>]))({(())(()[])}[<<>[]>{()[]}])]{<({{}{}}(<>{}))><[[<>[]]<[]()>]>}>{({{(<>()){
<<(<[(<<<{({[<()>{()()}][([]{})([][])]}{({{}}{<>{}})<(())[{}()]>})}[[{<[[][]]{()}){[{}<>]<{}(
<<{<{{[<[(({({[][]}<<><>>)}<((<><>)[{}[]])<<[]<>>>>)[{{[()[]][[]<>]}{({}())<(){}>}}])({[([[]<>](<><>))<
<<{<[(<(<([[{{()()}{<>()}}(<<>()>({}[]))]<<{{}{}}{()<>}>(({})(()()))>])[[([(<>[])(<>)](<(){
({[((({[([<{[{<><>}[()[]]][{[][]}{(){}}]>>(<{{[]{}}<()[]>}<{()<>}<[]()>>>[{({}<>)<{}()>}])])]<(<{{{<[]()
<[{<<{<<{<(({[<><>]{{}<>}})){<[{<>[]}({}{})]([{}()]{<>()})>[<{<><>}<<>{}>}([<>[]]<[]()>)]}>(
(<<(({<<(<(({[<>[]]<[][]>}({<>()}[[]()])){((<>[])){([]{}){[][]}}}){{[[[]{}][{}[]]][<{}[]>([]<>)]}[(({
<(<(<<<[<{({([<>()])[[[][]]{<>[]}]}[<[()]<{}<>>><[(){}]<()()>>])((<(()[])<{}{})>)((([][]))))}>]<[<([[<<>()
{{<[<<<{((([[([]{})][[<>[]]]])(<[[(){}]<<>[]>]>{[({}())([]<>)][<<>{}><[]()>]}))){{<[[[[][]
{([<{[<<[{({({<>{}}[[][]])[{(){}}{{}()}]}[[[{}()]<()()>]])({<[{}{}]>({{}{}}[[]{}])}{{({}())<()()>}{{<><>}<[]
[<{[{<{{<{<[{{{}{}}[[]()]}([{}[]]<{}()>)](<{()()}{<>[]}><(<>[])>)>}{{{((()<>)[<><>])[[()()]
([{[<[({([<<<([][])<{}{}>>[[()<>](<>{})]>{{({}<>)}([<><>]<()<>>)}>](<(<<[]<>>({}[])>[<<>{}>[()()]])(<<()[]>((
(<[{<({<([<({<<>()><{}()>}(<[]{}>({}<>)))[([[]()][<><>>){[<>[]]{{}<>}}]>({[{[]{}}({}[])]<{[
<<[<(<({<(<({<()><[]()>})>{[{[[]()]<{}>}<(()<>)(<>())>)(((()())[<><>])([(){}]))})([(<{<>()}([]())>{[{}{
[{<{<{<[<((({[()[]]{{}}}[[<><>]<<>{}>])<({[][]}{()[]})<([]())([]{})>>))((<([{}<>]{(){}})>[
[[[[([<{[[(<{{{}[]}[<>()]}<{[]}{<><>}>><{({}<>){()<>}}[{[]{}}{<>[]}]>)[([({}<>}([]<>)]{{{}{}
<{{[[<{[(({({<{}>[(){}]}(<[]()>))}<<<[{}{}]>(<[]{}>(<>[]))>([{{}<>}(()())]<{<><>}{<>()}>)>)<[<<
{[{{{(<<([([((()[])[[]<>])({{}{}}({}<>))]<({{}[]}{[][]})<{[][]}>>){<<({})[[]<>]>(<[][]>(<>{}))>}]<[
<[<(<{<[([[[{[()[]][<>{}]}[{(){}}{<><>}]]{<([]{}){()<>}>[({}<>)<<>[]>]}][<[({}[]){()[]}][<<>{}><()<>>]>]]<(
([<{(([[<<{([([]{})([]())]<{(){}}{[]<>}>)([<<>{}>[()()]]{[[]{}]<[]<>>})}[({{()()}<<>{}>>){[<()
([({{{[[[([{<{<><>}{()()}>(<[]())[<>])}{([<><>]<(){}>){(<>())[{}()]}}]<{[[<><>]{{}[]}]([{}<>]{[]()
[[<<<{{({[{[({{}{}}<(){}>)<[[]{}]({}<>)>]}][(<{<<>{}>}{{[]{}}{()[]}}>[{[<>{}]<[][]>}<{<><>
[([{(([{(([{[{[]()}[{}]][[[]<>]{<>{}}]}{[<{}{}>({}[])][<<><>>(()<>)]}]{<({[]{}}{<>{}}}>}){
(<<<<<[{<{({({[]{}}({}())){([][])[{}{}]}}([{<><>}{{}{}}][[{}[]]({}{})]))[[[{{}()}[<>{}]][{{}<>}{()}]
(([{<(<[(<{{{[[]<>](<>[])}{[()<>]<()()>}}}>)<{{({<{}<>>[{}{}]}({<>[]}[()()]))}([([<>[]][[]{}])(<<><>
((<{[(([{{([<[<>()]<<>[]>>{((){})([])}][<{[]()}<<>()>>(<{}()>(<>))])}[{{{{[]()}}{(<>[])[()<
<[[{{[<<({[<<{(){}}{(){}}>>(<{[]<>}<<>()>>({()<>}{{}<>}))]{[([[][]][{}<>])][((<>{})(()[]))[<()[]>{<>()}]]}
{<{<<[{[([{{{{{}()}<()<>>}<[()[]]>}<[<[]<>><()<>>](({}())<{}>)>}<<({[]()}(()()))({{}<>}{{}<>
{{[<{[<{[[{{({[]<>}<{}[]>)((<>{})({}{}))}([[[]{}]<{}[]>]<({}()){()<>}>)}[<([{}<>}<<><>>)><{[()[]][<>[]]}{
([{{{[{(<<<[<{<>[]}{()[]}>(([]())<<>{}>)]>[[(<{}()><[]<>>)][{[[][]]<(){}>}<<()()>({}[])>)]>>)<<[[(<(<>())((
<({[{{(({(<<<{[]()}(<>{})>{(()<>)[[]<>]}><[[<>()]]{([]{})[[]()]}>>)})({[{<(<[]<>>{{}[]])[<<>{}>[(){}]]><(({}(
((<(([{([{<[{<[]()>(()[])}<({})[<>[]]>]>}]{[<<{[[]()]<<>()>)<<{}[]>{<><>}>>>[[([(){}](<>[]))]{{<
[([<<([(<[<<([[]{}]<(){}>)([()[]][{}{}])>([([]<>)<(){}>]([[][]]{[]<>}))>{{[{[]<>}[{}[]]]{([]
[<{<[[<{<({<[<()<>><[]<>>][<{}<>><<>()>]><{((){})(()())}>}{[([{}<>]({}{})){(<>{})}]([[<><>](<>)])})><{<{
[(<[[<({{<<([<(){}><<><>}]<<{}()>({}<>)>){[(<>())[(){}]]<<<><>>[{}<>]>}><{[{(){}}][<{}{}>[()(
<[<{[{{<([{<[{()<>}{()<>}]]{<[{}<>](<>{})>([{}<>]{{}()})}}<[([(){}](()[]))<{{}{}}>]{{{<>}<[
(<{[[{[[{[[(<[[][]][<>{}]>(<()<>>(<>{})))[{([]{}}<{}[]>}[[[]{}]{()()}]]]{<[<<>{}>(()<>)]<{{}<>}<[
(<<<[[(<{<({{(<>())<(){}>}<(()())<()[]>>}<[({}{})[()[]]]>)>}{<{[<[()<>][<>[]]>[([]{})(()())]][({{}{}}
({({[[[(<<[{<<<>[]][{}()]>(({}())([]()))}]{<{[()[]]({}())}>({<()[]><[]{}>}[{<>()}<[]{}>])}>>)]]]}[({[[{({(((
((([[<{[(<({<{<><>}[[]]>[{(){}}>}((<()<>>{()()})<<<>()>({}())>))((([<>()]{[][]})[[(){}]<{}
<[{([(<[({[[([{}()]({}{}))([<><>]({}()))](({(){}}{()[]}><[[]()]<<><>>>)](<([(){}][{}{}])[{
{<((({<((<[[<{()[]}<{}{}>>](<<[]>{<>}><[[]{}]({})>)]<<{[{}{}]}[{<>{}}}>>><(([<<>>(()())]<(
{[[[<<(<{(<(<[{}<>]<[]()>>[{{}{}}{()<>}])[<{{}()}{{}{}}><{[]<>}({}())>]>)<<(<<()<>><<><>>>)>>}[(
[<{<[([({([[[{{}()}{{}()}]([[]()])]])}<<<[<<<>{}>[<><>)><{[]<>}(()<>)>](({{}<>}[()<>]){{<>[]}((){})})>({(
([[{{[[[<(<({(()())<{}[]>}<({}<>){[][]}>)[<{()()}[[]<>]>]>)([[({<>[]}[()()])[{[]<>}[{}{}]]][[<
({[(<<<{((<{<[()()][[]<>]>{<{}<>><()()>}}<{[()[]]([]<>)}<<{}()><{}{}>>>>[<<{(){}}[()[]]>[([]<
([({{{<[{[{{[([]{})<[]{}>][<<>[]>[{}[]]]}}[{[<<><>>{[][]}]((<>()){<>{}})}]]]][[(<({[[]()][[]{}]
(((<{{<<<([[{(<><>)<{}>}<{{}{}}[{}<>]>]{[(()())[{}<>}]}]{<(<[]()>)>([<()[]>{{}()}]<({}()){(){}}>)})<[({(<>()
[{(([[<(<<(<{<[]()>(<>{})}[{()[]}[()()]]><[<[]()>[[]<>]]>)<{<[[]<>]<()<>>>{[()[]]<{}[]>}}>>([((<[][
{<{<{[([(<<[{<()<>>(<>[])}[(<><>)[[]<>]]]<<<[]<>><(){}>>{{{}<>}<<><>>}>>(<{{<>()}[()<>]}><[";
    }
}
