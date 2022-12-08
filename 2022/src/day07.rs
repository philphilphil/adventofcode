use crate::base::{Problem, ProblemData};
use core::cell::RefCell;
use std::rc::Rc;

pub struct Day7 {}

#[derive(Debug)]
struct File {
    size: i32,
}

#[derive(Debug)]
struct Dir<'a> {
    pub name: String,
    pub files: Vec<File>,
    pub children: Vec<Dir<'a>>,
    pub parent: Option<Rc<RefCell<Dir<'a>>>>,
}

impl Dir<'static> {
    fn new(name: &str) -> Dir {
        Dir {
            name: name.to_owned(),
            files: vec![],
            children: vec![],
            parent: None,
        }
    }
}

impl Problem for Day7 {
    fn part_one(&self, problem_data: &ProblemData) -> String {
        let folder_tree = Dir::new("base");
        let folder_tree = Rc::new(RefCell::new(folder_tree));
        let mut current_dir = Rc::clone(&folder_tree);

        for line in problem_data.input.lines().skip(2) {
            if line.starts_with('$') {
                //command
                let mut split = line.split_whitespace();
                let first = split.nth(1).unwrap();
                let second = split.next().unwrap_or("");

                if first == "ls" {
                    continue;
                } else if first == "cd" {
                }
            } else {
                let mut split = line.split_whitespace();
                let first = split.next().unwrap();
                let second = split.next().unwrap();

                if line.starts_with("dir") {
                    let mut child = Dir::new(second);
                    child.parent = Some(Rc::clone(&current_dir));
                    current_dir.borrow_mut().children.push(child);
                } else {
                    // its a file
                    let filesize: i32 = first.parse().unwrap();
                    let file = File { size: filesize };
                    current_dir.borrow_mut().files.push(file);
                }
            }
        }

        dbg!(folder_tree);
        "".to_owned()
    }

    fn part_two(&self, problem_data: &ProblemData) -> String {
        "".to_owned()
    }
}
