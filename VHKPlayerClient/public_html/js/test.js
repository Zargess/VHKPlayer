function testXmlParser(testCase) {
    var result = parseXml(testCase.Parameter);
    if(result.length === testCase.Length) {
        return "succeded";
    } else {
        return "failed";
    }
}

function RunTest(testName, func, cases) {
    for (i = 0; i < cases.length; i++) {
        var result = func(cases[i]);
        console.log("Test " + testName + " " + result);
    }
}

function xmlParserTests() {
    var tests = [];
    
    var test1 = {
        Parameter: "<rss><Folder path='c:\\hello' marked='True'/><Folder path='c:\\hello' marked='True'/></rss>",
        Length: 2
    };
    
    tests.push(test1);
    
    RunTest("xml parsing", testXmlParser, tests);
}