use crate::base::{Problem, ProblemData};

pub struct Day8 {}

impl Problem for Day8 {
    fn part_one(&self, problem_data: &ProblemData) -> String {
        let tree_line = get_trees(&problem_data.input);
        let mut visible_trees = 0;

        for (curr_row, row) in tree_line.iter().enumerate() {
            'outer: for (curr_col, _) in row.iter().enumerate() {
                let tree = get_tree_at(&tree_line, curr_row as i32, curr_col as i32).unwrap();
                // println!("Row: {} Col: {} Tree: {}", curr_row, curr_col, tree);

                // Checking in all directions, if reaching None it means edge was
                // and the tree has a clear viewline

                // check up
                let mut search_up: i32 = curr_row as i32;
                loop {
                    search_up -= 1;
                    if let Some(above_tree) = get_tree_at(&tree_line, search_up, curr_col as i32) {
                        if above_tree >= tree {
                            break;
                        }
                    } else {
                        visible_trees += 1;
                        continue 'outer;
                    }
                }

                // Check Down
                let mut search_down: i32 = curr_row as i32;
                loop {
                    search_down += 1;
                    if let Some(above_tree) = get_tree_at(&tree_line, search_down, curr_col as i32)
                    {
                        if above_tree >= tree {
                            break;
                        }
                    } else {
                        visible_trees += 1;
                        continue 'outer;
                    }
                }

                // Check Left
                let mut search_left: i32 = curr_col as i32;
                loop {
                    search_left -= 1;
                    if let Some(left_tree) = get_tree_at(&tree_line, curr_row as i32, search_left) {
                        if left_tree >= tree {
                            break;
                        }
                    } else {
                        visible_trees += 1;
                        continue 'outer;
                    }
                }

                // Check Right
                let mut search_right: i32 = curr_col as i32;
                loop {
                    search_right += 1;
                    if let Some(right_tree) = get_tree_at(&tree_line, curr_row as i32, search_right)
                    {
                        if right_tree >= tree {
                            break;
                        }
                    } else {
                        visible_trees += 1;
                        continue 'outer;
                    }
                }
            }
        }

        visible_trees.to_string()
    }

    fn part_two(&self, problem_data: &ProblemData) -> String {
        "".to_owned()
    }
}

fn get_tree_at(trees: &Vec<Vec<i8>>, row: i32, col: i32) -> Option<i8> {
    if row as usize >= trees.len() || col as usize >= trees[0].len() {
        None
    } else {
        Some(trees[row as usize][col as usize])
    }
}

fn get_trees(input: &str) -> Vec<Vec<i8>> {
    let mut trees = vec![];

    for line in input.lines() {
        let mut tree_line = vec![];
        for char in line.chars() {
            tree_line.push(char.to_digit(10).unwrap() as i8);
        }
        trees.push(tree_line);
    }
    trees
}
