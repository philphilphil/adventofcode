use crate::base::{Problem, ProblemData};
use anyhow::Result;
use regex::Regex;

pub struct Day11 {}

impl Problem for Day11 {
    fn part_one(&self, problem_data: &ProblemData) -> String {
        let mut monkeys: Vec<Monkey> = parse_monkeys(&problem_data.input).expect("Parse issue.");

        for _ in 1..=20 {
            for i in 0..monkeys.len() {
                while let Some(result) = monkeys[i].inspect(true) {
                    monkeys[result.0 as usize].items.push(result.1);
                }
            }
        }

        monkeys.sort_by(|a, b| b.inspect_count.cmp(&a.inspect_count));
        let result = monkeys[0].inspect_count * monkeys[1].inspect_count;
        result.to_string()
    }

    fn part_two(&self, problem_data: &ProblemData) -> String {
        let mut monkeys: Vec<Monkey> = parse_monkeys(&problem_data.input).expect("Parse issue.");

        for _ in 1..=10000 {
            for i in 0..monkeys.len() {
                while let Some(result) = monkeys[i].inspect(false) {
                    monkeys[result.0 as usize].items.push(result.1);
                }
            }
        }

        monkeys.sort_by(|a, b| b.inspect_count.cmp(&a.inspect_count));
        let result = monkeys[0].inspect_count * monkeys[1].inspect_count;
        result.to_string()
    }
}

fn parse_monkeys(input: &str) -> Result<Vec<Monkey>> {
    let mut monkeys = vec![];
    let monkey_strings = split_monkeys(input);

    for m in monkey_strings {
        let mut lines = m.lines();
        let mut monkey = Monkey::default();

        // parse number
        let regex = Regex::new(r"Monkey (?P<number>\d):")?;
        let result = regex.captures(lines.next().unwrap()).unwrap();
        monkey.number = result["number"].parse()?;

        // parse starting items
        let starting_items = lines.next().unwrap().split(':').nth(1).unwrap().trim();
        monkey.items = starting_items
            .split(',')
            .map(|s| s.trim().parse::<u64>().unwrap())
            .collect();

        monkey.items.reverse();

        // parse operation
        let regex = Regex::new(r"(?m)  Operation: new = old (?P<op>\*|\+) (?P<num>old|\d*)")?;
        let result = regex.captures(lines.next().unwrap()).unwrap();
        match &result["op"] {
            "*" => monkey.operation = Some(Operation::Multiply),
            "+" => monkey.operation = Some(Operation::Plus),
            _ => panic!("Unknown operation."),
        }
        match &result["num"] {
            "old" => monkey.operation_num = None,
            x => monkey.operation_num = Some(x.parse()?),
        }

        // parse test
        let regex = Regex::new(r"(?m)  Test: divisible by (?P<num>\d*)")?;
        let result = regex.captures(lines.next().unwrap()).unwrap();
        monkey.test_num = result["num"].parse()?;

        // parse test true/false monkeys
        let regex = Regex::new(r"(?m)If true: throw to monkey (?P<num>\d*)")?;
        let result = regex.captures(lines.next().unwrap()).unwrap();
        monkey.test_result_true_monkey = result["num"].parse()?;

        let regex = Regex::new(r"(?m)If false: throw to monkey (?P<num>\d*)")?;
        let result = regex.captures(lines.next().unwrap()).unwrap();
        monkey.test_result_false_monkey = result["num"].parse()?;

        monkeys.push(monkey);
    }

    Ok(monkeys)
}

// TODO: this for sure can be done simpler..
fn split_monkeys(input: &str) -> Vec<String> {
    let mut monkeys = vec![];

    let mut monkey = String::new();
    for s in input.lines() {
        if s.is_empty() {
            monkeys.push(monkey);
            monkey = String::new();
            continue;
        }
        monkey += s;
        monkey += "\r\n";
    }
    monkeys.push(monkey);

    monkeys
}

#[derive(Debug, Default)]
struct Monkey {
    number: u64,
    items: Vec<u64>,
    operation: Option<Operation>,
    operation_num: Option<u64>,
    test_num: u64,
    test_result_true_monkey: u64,
    test_result_false_monkey: u64,
    inspect_count: u64,
}

#[derive(Debug)]
enum Operation {
    Plus,
    Multiply,
}

impl Monkey {
    // inspect and return monkey to throw to and what number
    fn inspect(&mut self, part_one: bool) -> Option<(u64, u64)> {
        if self.items.is_empty() {
            return None;
        }
        self.inspect_count += 1;

        // pop item to inspect
        let mut worry_level: u64 = self.items.pop().unwrap();

        // set operation number value
        let operation_number = if let Some(op_num) = self.operation_num {
            op_num
        } else {
            worry_level
        };

        // dbg!(self.number);
        // dbg!(worry_level, operation_number);
        // calculate operation
        match self.operation.as_ref().unwrap() {
            Operation::Plus => worry_level += operation_number,
            Operation::Multiply => worry_level *= operation_number,
        }

        if part_one {
            worry_level /= 3;
        }

        // execute test and return new monkey and worry_level
        if worry_level % self.test_num == 0 {
            Some((self.test_result_true_monkey, worry_level % 9699690))
        } else {
            Some((self.test_result_false_monkey, worry_level % 9699690))
        }
    }
}
