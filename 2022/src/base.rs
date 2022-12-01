use std::fs;

pub trait Problem {
    fn part_one(&self, problem_data: &ProblemData) -> String;
    fn part_two(&self, problem_data: &ProblemData) -> String;
}

#[derive(Debug)]
pub struct ProblemData {
    pub input: String,
    pub p1_result: String,
    pub p2_result: String,
    pub is_example: bool,
    pub day: u8,
}

impl ProblemData {
    pub fn new(day: u8, run_example: bool) -> ProblemData {
        let answers = get_answers(day, run_example);
        ProblemData {
            day,
            is_example: run_example,
            input: load_problem_input(day, run_example),
            p1_result: answers.0,
            p2_result: answers.1,
        }
    }
}

fn load_problem_input(day: u8, run_example: bool) -> String {
    let filename = get_day_path(day, run_example);
    fs::read_to_string(filename).expect("Unable to read file")
}

fn get_day_path(day_number: u8, example: bool) -> String {
    let mut path = get_day_base_path(day_number);
    if example {
        path.push_str("_example");
    }

    path
}

pub fn get_answers(day_number: u8, example: bool) -> (String, String) {
    let mut path = get_day_base_path(day_number);
    path.push_str("_answers");
    let answers = fs::read_to_string(path).expect("Unable to read file");
    let mut answers = answers.lines();

    let (p1_line, p2_line) = match example {
        false => (1, 4), //those numbers does not match the lines becasue nth consumes
        true => (3, 4),
    };

    let p1 = answers.nth(p1_line).to_owned().unwrap_or("");
    let p2 = answers.nth(p2_line).to_owned().unwrap_or("");
    (p1.to_owned(), p2.to_owned())
}

fn get_day_base_path(day_number: u8) -> String {
    let mut path: String = "Inputs/".to_owned();
    if day_number <= 9 {
        path.push('0');
    }
    path.push_str(&day_number.to_string());
    path
}
