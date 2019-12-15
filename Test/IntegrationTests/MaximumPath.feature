 Feature: Find maximum path

@MP-1
Scenario Outline: Simple examples from specification
Given pyramid is defined in '<inputFile>'
And smart maximum path finder algorithm is used
When algorithm  is executed
Then the output is equal to '<outputFile>' content

Examples:
| inputFile  | outputFile  |
| input1.txt | output1.txt |
| input2.txt | output2.txt |

@MP-2
Scenario: Avoid local maximum
Given pyramid is defined in 'input3.txt'
And smart maximum path finder algorithm is used
When algorithm  is executed
Then the output is equal to 'output3.txt' content

@MP-3
Scenario: There is no valid path
Given pyramid is defined in 'input4.txt'
And smart maximum path finder algorithm is used
When algorithm  is executed
Then the output is equal to 'output4.txt' content

@MP-5
Scenario Outline: Small pyramide
Given pyramid is defined in '<inputFile>'
And smart maximum path finder algorithm is used
When algorithm  is executed
Then the output is equal to '<outputFile>' content

Examples:
| inputFile  | outputFile  |
| input5.txt | output5.txt |
| input6.txt | output6.txt |

@MP-6
Scenario: There are multiple paths with same maximum value
Given pyramid is defined in 'input7.txt'
And smart maximum path finder algorithm is used
When algorithm  is executed
Then the output is equal to 'output7.txt' content